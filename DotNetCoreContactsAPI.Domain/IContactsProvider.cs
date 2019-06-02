using System;
using System.Collections.Generic;
using System.Text;
using DotNetCoreContactsAPI.Domain.Requests;

namespace DotNetCoreContactsAPI.Domain
{
    public interface IContactsProvider
    {
        Contact FetchContact(string id);
        Contact InsertContact(CreateContactRequest request);
        bool DeleteContact(string id);
    }
}
