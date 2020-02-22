using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CityGovernance.Domain.Models;
using CityGovernance.infra.Configurations;
using CityGovernance.Infra.Db;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CityGovernance
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
            services
                 .AddDbContext<CityGovernanceContext>(opt =>
                 {
                     opt.UseNpgsql(Configuration.GetConnectionString(nameof(CityGovernanceContext)));


                 })
                .AddControllersWithViews();
            //.SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            DependencyInjectionConfiguratino(services);

        }

        private static void DependencyInjectionConfiguratino(IServiceCollection services)
        {
            services.AddTransient<IDataService, DataService>();

        }



        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });


            serviceProvider.GetService<IDataService>().Seed();

        }
    }
}
