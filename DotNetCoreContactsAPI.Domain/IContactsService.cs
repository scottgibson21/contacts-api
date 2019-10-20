using DotNetCoreContactsAPI.Domain.Requests;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DotNetCoreContactsAPI.Domain
{
    public interface IContactsService
    {
        Task<Contact> Create(Contact request);
        Contact FetchContact(string id);
        bool DeleteContact(string id);
    }
}
