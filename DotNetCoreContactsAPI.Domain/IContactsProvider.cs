using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DotNetCoreContactsAPI.Domain.Requests;

namespace DotNetCoreContactsAPI.Domain
{
    public interface IContactsProvider
    {
        Contact FetchContact(string id);
        Task<Contact> InsertContact(Contact request);
        bool DeleteContact(string id);
    }
}
