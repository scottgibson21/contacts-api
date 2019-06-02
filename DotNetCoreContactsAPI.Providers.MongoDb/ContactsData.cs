using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DotNetCoreContactsAPI.Providers.MongoDb
{
    public class ContactsData
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonElement("FirstName")]
        public string FirstName { get; set; }
        [BsonElement("Lastname")]
        public string LastName { get; set; }
        [BsonElement("PhoneNumber")]
        public long PhoneNumber { get; set; }
        [BsonElement("Address")]
        public string Address { get; set; }
        [BsonElement("City")]
        public string City { get; set; }
        [BsonElement("State")]
        public string State { get; set; }
        [BsonElement("Zip")]
        public string Zip { get; set; }
    }
}
