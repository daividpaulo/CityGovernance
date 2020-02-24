using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CityGovernance.Domain.Interfaces;
using CityGovernance.Domain.Models;
using CityGovernance.infra.Configurations;
using CityGovernance.infra.Db;
using CityGovernance.infra.Interfaces;
using CityGovernance.infra.Repositories;
using CityGovernance.Infra.Db;
using CityGovernance.Middlewares;
using CityGovernance.Services.Services;
using CityGovernance.ViewModels;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ReflectionIT.Mvc.Paging;

namespace CityGovernance
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

       
        public void ConfigureServices(IServiceCollection services)
        {
            services
                 .AddDbContext<CityGovernanceContext>(opt =>
                 {
                     opt.UseNpgsql(Configuration.GetConnectionString(nameof(CityGovernanceContext)));


                 }).AddControllersWithViews();

            DependencyInjectionConfiguratino(services);
            AutoMapperConfiguration(services);

        }

        private void AutoMapperConfiguration(IServiceCollection services)
        {
            var config = new AutoMapper.MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CityViewModel, City>();
                cfg.CreateMap<City, CityViewModel>();
                cfg.CreateMap<RegionViewModel, Region>();
                cfg.CreateMap<Region, RegionViewModel>();
            });

            IMapper mapper = config.CreateMapper();
            services.AddSingleton(mapper);
        }

        private static void DependencyInjectionConfiguratino(IServiceCollection services)
        {
            services.AddScoped<IDataService, DataService>();
            services.AddScoped<ICityService, CityService>();
            services.AddScoped<ICityRepository, CityRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider)
        {

            if (env.IsDevelopment())
            { 
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();
            app.UseUow();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Cities}/{action=SearchCities}/{id?}");
            });

            serviceProvider.GetService<IDataService>().Seed();
        }
    }
}
