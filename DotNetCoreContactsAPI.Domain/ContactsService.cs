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
            var response = await _contactsProvider.InsertContact(request);
            return response;
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
