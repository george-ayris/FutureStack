using FutureStack.Core.Adaptors;

namespace FutureStack.Api
{
    public class Config : IConfig
    {
        public string Key1 { get; }
        public string ConnectionString { get; }

        public Config(string key1, string connectionString)
        {
            Key1 = key1;
            ConnectionString = connectionString;
        }
    }
}