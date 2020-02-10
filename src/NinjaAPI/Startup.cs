using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using NinjaAPI.Models;
using NinjaAPI.Repositories;
using NinjaAPI.Services;
using System.Collections.Generic;

namespace NinjaAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.TryAddSingleton<IClanService, ClanService>();
            services.TryAddSingleton<IClanRepository, ClanRepository>();
            services.TryAddSingleton<IEnumerable<Clan>>(new Clan[] {
                new Clan { Name = "Marvel" },
                new Clan { Name = "DC Comics "}
            });
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
