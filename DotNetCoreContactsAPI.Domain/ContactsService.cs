using System;
using System.Threading.Tasks;
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

        public async Task<Contact> Create(Contact request)
        {
            return await _contactsProvider.InsertContact(request);
        }

        public Contact FetchContact(string id)
        {
            return _contactsProvider.FetchContact(id);
        }

        public bool DeleteContact(string id)
        {
            return _contactsProvider.DeleteContact(id);
        }
    }
}
