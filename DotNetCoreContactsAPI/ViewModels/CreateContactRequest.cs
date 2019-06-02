using System.ComponentModel.DataAnnotations;


namespace DotNetCoreContactsAPI.Controllers
{
    public class CreateContactRequest
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public long PhoneNumber { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        [StringLength(2, ErrorMessage = "State field must be exaclty 2 characters")]
        public string State { get; set; }
        [Required]
        public string Zip { get; set; }
    }
}