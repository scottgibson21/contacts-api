using DotNetCoreContactsAPI;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Text;

namespace DotNetCoreContactsApi.Tests.Behavioral.Common
{
    public class Specification
    {
        public IWebHostBuilder CreateWebHostBuilder()
        {
            return Program.CreateWebHostBuilder();
        }
    }
}
