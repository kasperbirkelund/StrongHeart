using System;

namespace StrongHeart.DemoApp.WebApi.Services
{
    public class ConfigurationReaderImpl : IConfigurationReader
    {
        public bool GetValue()
        {
            return DateTime.Now.Second % 2 == 0;
        }
    }
}