using DotNetCoreContactsAPI.Domain.Requests;
using System;
using System.Collections.Generic;
using System.Text;

namespace DotNetCoreContactsAPI.Domain
{
    public interface IContactsService
    {
        Contact Create(CreateContactRequest request);
        Contact FetchContact(string id);

        bool DeleteContact(string id);
    }
}
