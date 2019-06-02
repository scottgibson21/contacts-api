using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DotNetCoreContactsAPI.ViewModels
{
    public class GetContactRequest
    {
        [Required]
        public string ContactId { get; set; }
    }
}
