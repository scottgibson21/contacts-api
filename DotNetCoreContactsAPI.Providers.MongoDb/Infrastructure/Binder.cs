using DotNetCoreContactsAPI.Domain;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace DotNetCoreContactsAPI.Providers.MongoDb.Infrastructure
{
    public static class Binder
    {
        public static IServiceCollection RegisterMongoProvider(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetValue<string>("MongoDB:ConnectionString");
            services.AddSingleton<IMongoClient>(new MongoClient(configuration.GetSection("MongoDB:ConnectionString").Value));
            services.AddSingleton(provider => provider.GetService<IMongoClient>().GetDatabase(MongoDbConstants.DatabaseName));
            services.AddSingleton(provider => provider.GetService<IMongoDatabase>().GetCollection<ContactsData>(MongoDbConstants.CollectionName));

            services.AddSingleton<IContactsProvider, ContactsProvider>();

            return services;

        }

    }
}
