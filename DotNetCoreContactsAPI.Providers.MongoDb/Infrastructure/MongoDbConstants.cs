using System;
using System.Collections.Generic;
using System.Text;

namespace DotNetCoreContactsAPI.Providers.MongoDb.Infrastructure
{
    public static class MongoDbConstants
    {
        public const string ConnectionString = "MongoDb:ConnectionString";
        public const string DatabaseName = "ContactsDb";
        public const string CollectionName = "contacts";
    }
}
