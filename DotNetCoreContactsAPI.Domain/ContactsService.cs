using System;
using DotNetCoreContactsAPI.Domain.Requests;

namespace DotNetCoreContactsAPI.Domain
{
    public class ContactsService : IContactsService
    {

        private readonly IContactsProvider _contactsProvider;

        public ContactsService(IContactsProvider contactsProvider)
        {
            _contactsProvider = contactsProvider;
        }

        public Contact Create(CreateContactRequest request)
        {
            return _contactsProvider.InsertContact(request);
        }

        public Contact FetchContact(string id)
        {
            var result = _contactsProvider.FetchContact(id);

            return result;
        }

        public bool DeleteContact(string id)
        {
            return _contactsProvider.DeleteContact(id);
        }
    }
}
