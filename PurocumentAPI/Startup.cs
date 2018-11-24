﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using DevelopBase.Common;
namespace PurocumentAPI
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
            services.AddCors(Options=>{
                Options.AddPolicy("default",builder=>{
                    builder.AllowAnyHeader();
                    builder.AllowAnyMethod();
                    builder.AllowAnyOrigin();
                    builder.AllowCredentials();
                });
            });
            var dbcontextRegisterInfo=Configuration.GetSection("Database").Get<IEnumerable<RegisterInfo>>();
            services.AddDbcontext(dbcontextRegisterInfo);
            var serviceRegisterInfo=Configuration.GetSection("Service").Get<IEnumerable<RegisterInfo>>();
            services.AddServices(serviceRegisterInfo);
            var handlerRegisterInfo=Configuration.GetSection("Handler").Get<IEnumerable<RegisterInfo>>();
            services.AddHandlers(handlerRegisterInfo);
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
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
                app.UseHsts();
            }
            app.UseCors("default");
            app.UseHttpsRedirection();
            app.UseMvc(routes=>{
                routes.MapRoute(name:"default",template:"api/{controller}/{action}");
            });
        }
    }
}
