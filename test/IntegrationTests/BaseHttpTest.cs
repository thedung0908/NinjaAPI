using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using NinjaAPI;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace IntegrationTests
{
    public abstract class BaseHttpTest : IDisposable
    {
        protected TestServer Server { get; }
        protected HttpClient Client { get; }

        protected virtual Uri BaseAddress => new Uri("http://locahost");
        protected virtual string Environment => "Development";
        public BaseHttpTest()
        {
            var builder = new WebHostBuilder()
                .UseEnvironment(Environment)
                .ConfigureServices(ConfigureServices)
                .UseStartup<Startup>();

            Server = new TestServer(builder);
            Client = Server.CreateClient();
            Client.BaseAddress = BaseAddress;
        }

        protected virtual void ConfigureServices(IServiceCollection services)
        {
        }
        #region IDisposable support
        private bool disposedValue = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    Client.Dispose();
                    Server.Dispose();
                }
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }
        #endregion
    }
}
