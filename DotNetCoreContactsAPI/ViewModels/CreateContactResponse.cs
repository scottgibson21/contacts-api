using System.ComponentModel.DataAnnotations;

namespace DotNetCoreContactsAPI.Controllers
{
    public class CreateContactResponse
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public long PhoneNumber { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
    }
}