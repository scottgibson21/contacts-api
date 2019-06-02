using System;
using System.Collections.Generic;
using System.Text;

namespace DotNetCoreContactsAPI.Domain.Requests
{
    public class CreateContactRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public long PhoneNumber { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
    }
}
