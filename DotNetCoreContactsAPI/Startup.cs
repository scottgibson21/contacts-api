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
using DotNetCoreContactsAPI.Domain;
using DotNetCoreContactsAPI.Providers.MongoDb;
using Swashbuckle.AspNetCore.Swagger;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using IdentityServer4.AccessTokenValidation;

using DotNetCoreContactsAPI.Providers.MongoDb.Infrastructure;

namespace DotNetCoreContactsAPI
{
    public class Startup
    {

        public IConfiguration Configuration { get; set; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "title", Version = "v1" });

                var xmlPath = System.AppDomain.CurrentDomain.BaseDirectory + @"DotNetCoreContactsAPI.xml";
                c.IncludeXmlComments(xmlPath);
            });
            services.Configure<AppSettingsTest>(Configuration.GetSection("AppSettingsTest"));

            services.AddSingleton<IConfiguration>(Configuration);

            services.Configure<MongoDbSettings>(
                    options =>
                    {
                        options.ConnectionString = Configuration.GetSection("MongoDB:ConnectionString").Value;
                        options.Database = Configuration.GetSection("MongoDB:Database").Value;
                        options.Collection = Configuration.GetSection("MongoDb:Collection").Value;
                    }
                );
            services.AddSingleton<IContactsService, ContactsService>();
            services.RegisterMongoProvider(Configuration);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Contacts API");
                c.RoutePrefix = String.Empty;
            });
        }
    }
}
