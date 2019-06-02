using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace DotNetCoreContactsAPI.Errors
{
    public class ErrorTest
    {
        public int StatusCode { get; set; }
        public string ErrorMessage { get; set; }
    }
}
