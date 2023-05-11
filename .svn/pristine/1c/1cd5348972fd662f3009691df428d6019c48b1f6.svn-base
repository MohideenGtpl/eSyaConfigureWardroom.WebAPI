using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConfigureWardRoom.DL.Repository;
using ConfigureWardRoom.Filters;
using ConfigureWardRoom.IF;
using ConfigureWardRoom.WebAPI.Extention;
using ConfigureWardRoom.WebAPI.Utility;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using DL_ConfigureWardRoom = ConfigureWardRoom.DL.Entities;
namespace ConfigureWardRoom.WebAPI
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

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            DL_ConfigureWardRoom.eSyaEnterpriseContext._connString = Configuration.GetConnectionString("dbConn_eSyaEnterprise");
           
            //for Read connection string
            services.AddMvc(options =>
            {
                options.Filters.Add(typeof(HttpAuthAttribute));
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_2);



            //Registering Api key filters
            services.AddMvc(options =>
            {
                options.Filters.Add(typeof(ApikeyAuthAttribute));
                // An instance
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            //for cross origin support
            services.AddCors(c =>
            {
                c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin());
            });

            //uncomment if you want to Remove API KEY Authentication

            //services.AddMvc(options =>
            //{
            //   options.Filters.Add(typeof(HttpAuthAttribute));
            //}).SetCompatibilityVersion(CompatibilityVersion.Version_2_2);


            //services.AddDbContext<eSyaEnterpriseContext>(options =>
            //options.UseSqlServer(Configuration.GetConnectionString("dbContext")));


            services.AddScoped<IRoomLocationRepository, RoomLocationRepository>();
            services.AddScoped<IRoomMasterRepository, RoomMasterRepository>();
            services.AddScoped<IWardMasterRepository, WardMasterRepository>();
            services.AddScoped<IWardRoomLinkRepository, WardRoomLinkRepository>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.ConfigureExceptionHandler();

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
