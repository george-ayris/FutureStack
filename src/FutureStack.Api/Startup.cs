using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Consul;
using FutureStack.Core.Adaptors;
using FutureStack.Core.Ports.WriteSide;
using FutureStack.Core.Ports.ReadSide;
using FutureStack.Core.Adaptors.Repositories;
using FutureStack.Persistence;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SimpleInjector;
using SimpleInjector.Integration.AspNetCore;
using SimpleInjector.Integration.AspNetCore.Mvc;

namespace FutureStack.Api
{
    public class Startup
    {
        private Container _container = new Container();
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddCors();
            services.AddMvc();

            services.AddSingleton<IControllerActivator>(
                new SimpleInjectorControllerActivator(_container));
            services.AddSingleton<IViewComponentActivator>(
                new SimpleInjectorViewComponentActivator(_container));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            app.UseSimpleInjectorAspNetRequestScoping(_container);
            _container.Options.DefaultScopedLifestyle = new AspNetRequestLifestyle();
            InitializeContainer(app);
            _container.Verify();

            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseCors(builder => builder
                .AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod());

            app.UseMvc();
        }

        private void InitializeContainer(IApplicationBuilder app)
        {
            var config = GetConfig();
            _container.Register<IConfig>(() => config, Lifestyle.Singleton);

            // Add application presentation components:
            _container.RegisterMvcControllers(app);
            _container.RegisterMvcViewComponents(app);

            _container.Register<IWriteTodos, WriteTodosAppServiceDelegator>(Lifestyle.Scoped);
            _container.Register<ICreateTodos, CreateTodosAppService>(Lifestyle.Scoped);
            _container.Register<IDeleteTodos, DeleteTodosAppService>(Lifestyle.Scoped);
            _container.Register<IDeleteAllTodos, DeleteAllTodosAppService>(Lifestyle.Scoped);
            _container.Register<IReadTodos, ReadTodosQueryDelegetor>(Lifestyle.Scoped);
            _container.Register<IGetAllTodos, GetAllTodosQuery>(Lifestyle.Scoped);
            _container.Register<IGetTodos, GetTodoQuery>(Lifestyle.Scoped);
            _container.Register<ITodoRepository, TodoRepository>(Lifestyle.Singleton);

            // Cross-wire ASP.NET services (if any). For instance:
            //_container.RegisterSingleton(app.ApplicationServices.GetService<ILoggerFactory>());
        }

        private Config GetConfig()
        {
            Task<QueryResult<KVPair>> key1Task = null;
            Task<QueryResult<KVPair>> connectionStringTask = null;
            bool consulCallFailed = false;
            try
            {
                var client = new ConsulClient(c => c.Address = new Uri(Environment.GetEnvironmentVariable("CONSUL_URL")));
                key1Task = client.KV.Get("web/key1");
                connectionStringTask = client.KV.Get("web/sqlConnectionString");
                Task.WaitAll(key1Task, connectionStringTask);
            }
            catch 
            {
                consulCallFailed = true;
            }

            string key1;
            if (!consulCallFailed && key1Task.Result.Response?.Value != null)
            {
                key1 = Encoding.UTF8.GetString(key1Task.Result.Response.Value);
            }
            else
            {
                key1 = "Cannot get info from consul";
            }


            string connectionString;
            if (!consulCallFailed && connectionStringTask.Result.Response?.Value != null)
            {
                var connectionFormatString = Encoding.UTF8.GetString(connectionStringTask.Result.Response.Value);
                var sqlUserName = Environment.GetEnvironmentVariable("SQL_USERNAME");
                var sqlPassword = Environment.GetEnvironmentVariable("SQL_PASSWORD");
                connectionString = string.Format(connectionFormatString, sqlUserName, sqlPassword);
            }
            else
            {
                connectionString = "Server=tcp:localhost,1433";
            }

            return new Config(key1, connectionString);
        }
    }
}
