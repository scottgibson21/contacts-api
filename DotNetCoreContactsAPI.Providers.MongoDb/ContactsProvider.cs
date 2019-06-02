using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson;
using MongoDB.Driver;
using DotNetCoreContactsAPI.Domain;
using DotNetCoreContactsAPI.Domain.Requests;
using Microsoft.Extensions.Options;
using System.Net.Http;

namespace DotNetCoreContactsAPI.Providers.MongoDb
{
    public class ContactsProvider : IContactsProvider
    {
        private readonly IMongoCollection<ContactsData> _contacts;

        public ContactsProvider(IMongoCollection<ContactsData> contactsCollection)
        {
            _contacts = contactsCollection;
        }

        public Contact InsertContact(CreateContactRequest request)
        {
            var contact = MapToMongoContact(request);
             _contacts.InsertOne(contact);

            var insertedContact = new Contact
            {
                Id = contact.Id,
                FirstName = contact.FirstName,
                LastName = contact.LastName,
                PhoneNumber = contact.PhoneNumber,
                Address = contact.Address,
                City = contact.City,
                State = contact.State,
                Zip = contact.Zip
            };

            return insertedContact;


        }

        public Contact FetchContact(string id)
        {
            
            var result = _contacts.FindSync(contact => contact.Id == id).FirstOrDefault();

            if (result == null)
            {
                return null;
            }

            var mappedResult = new Contact
            {
                Id = result.Id,
                FirstName = result.FirstName,
                LastName = result.LastName,
                PhoneNumber = result.PhoneNumber,
                Address = result.Address,
                City = result.City,
                State = result.State,
                Zip = result.Zip
            };

            return mappedResult;
        }

        public bool DeleteContact(string id)
        {
            var result = _contacts.DeleteOne(x => x.Id == id);

            if (result.DeletedCount == 0)
            {
                return false;
            }

            return true;

        }

        #region helpers

        private ContactsData MapToMongoContact(CreateContactRequest request)
        {
            return new ContactsData
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Address = request.Address,
                City = request.City,
                State = request.State,
                Zip = request.Zip,
                PhoneNumber = request.PhoneNumber,
            };
        }

        #endregion

    }
}
