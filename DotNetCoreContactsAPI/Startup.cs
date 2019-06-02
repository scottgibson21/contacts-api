using System;
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Contacts API", Description = "API for managing contacts" });

                var xmlPath = System.AppDomain.CurrentDomain.BaseDirectory + @"DotNetCoreContactsAPI.xml";
                c.IncludeXmlComments(xmlPath);
            });
            services.Configure<AppSettingsTest>(Configuration.GetSection("AppSettingsTest"));
            var test = Configuration.GetSection("TestValue");
            var test2 = Configuration.GetSection("TestValue2:NestedTestValue:ExtraNestedValue");
            var test3 = Configuration.GetSection("TestValue2").GetChildren();
            var test4 = Configuration.GetSection("Section7");
            var test5 = test4.GetChildren();

            var section7 = new Section7();
            Configuration.GetSection("Section7").Bind(section7);

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
            services
                .AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
                .AddJwtBearer(IdentityServerAuthenticationDefaults.AuthenticationScheme, options =>
                {
                    options.Audience = "test";
                    options.Authority = "test";
                    options.RequireHttpsMetadata = false;
                });
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
            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseMvc();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Contacts API");
            }
                
                
                );
        }
    }
}
