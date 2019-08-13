using DotNetCoreContactsApi.Tests.Behavioral.Common;
using System;
using System.Net.Http;
using Xunit;
using Moq;
using DotNetCoreContactsAPI.Domain;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.AspNetCore.Hosting;
using DotNetCoreContactsAPI;
using Microsoft.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using DotNetCoreContactsAPI.ViewModels;
using System.Net.Http.Headers;

namespace DotNetCoreContactsApi.Tests.Behavioral
{
    public class CreatContactSpecification : Specification
    {
        private const string Url = "api/Contacts";
        public HttpClient Client { get; set; }
        public Mock<IContactsProvider> ContactsProviderMock { get; set; }
        private void Setup()
        {
            var builder = WebHost.CreateDefaultBuilder(null).UseStartup<Startup>();

            ContactsProviderMock = new Mock<IContactsProvider>();
            var contactProviderDescriptor = new ServiceDescriptor(typeof(IContactsProvider), ContactsProviderMock.Object);
            builder.ConfigureTestServices(sc => sc.Replace(contactProviderDescriptor));

            var testServer = new TestServer(builder);
            Client = testServer.CreateClient();

            Client.DefaultRequestHeaders
              .Accept
              .Add(new MediaTypeWithQualityHeaderValue("application/json"));

        }

        [Fact]
        public void Test1()
        {
            //given I have a test server and client
            Setup();

            //given I have a valid request
            var id = "contactId";

            //given I have a contact to return
            var contact = new Contact
            {
                FirstName = "test",
                LastName = "client"
            };

            ContactsProviderMock.Setup(a => a.FetchContact(id))
                .Returns(contact);

            //when I fetch the client
           var response = Client.GetAsync(Url + $"/{id}").Result;

            //then
            Assert.NotNull(response);
                
        }
    }
}
