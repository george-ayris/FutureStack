using FutureStack.Core.Ports;
using FutureStack.Core.Adaptors.Repositories;
using FutureStack.Persistance;
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
            // Add application presentation components:
            _container.RegisterMvcControllers(app);
            _container.RegisterMvcViewComponents(app);

            _container.Register<ITodosPort, TodosAppServiceDelegator>(Lifestyle.Scoped);
            _container.Register<ICreateTodos, CreateTodosAppService>(Lifestyle.Scoped);
            _container.Register<ITodoRepository, TodoRepository>(Lifestyle.Singleton);

            // Cross-wire ASP.NET services (if any). For instance:
            //_container.RegisterSingleton(app.ApplicationServices.GetService<ILoggerFactory>());
        }
    }
}
