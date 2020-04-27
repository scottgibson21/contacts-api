using DotNetCoreContactsApi.Tests.Behavioral.Common;
using DotNetCoreContactsAPI;
using DotNetCoreContactsAPI.Controllers;
using DotNetCoreContactsAPI.Domain;
using FluentAssertions;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Moq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Xunit;

namespace DotNetCoreContactsApi.Tests.Behavioral
{
    public class GetContactSpecification : Specification
    {
        private const string Url = "api/Contacts/";
        public HttpClient Client { get; set; }
        public Mock<IContactsProvider> ContactsProviderMock { get; set; }
        private void Setup()
        {
            var builder = Program.CreateWebHostBuilder();

            ContactsProviderMock = new Mock<IContactsProvider>();
            var contactProviderDescriptor = new ServiceDescriptor(typeof(IContactsProvider), ContactsProviderMock.Object);
            builder.ConfigureTestServices(sc => sc.Replace(contactProviderDescriptor));

            var testServer = new TestServer(builder);
            Client = testServer.CreateClient();

            Client.DefaultRequestHeaders
              .Accept
              .Add(new MediaTypeWithQualityHeaderValue("application/json"));

        }

        [Fact(DisplayName = "Create contact should return 200 for valid request")]
        public async Task Test1()
        {
            //given I have a test server and client
            Setup();

            //given I have a valid request
            var contact = new Contact
            {
                Id = "ContactId",
                FirstName = "testfirst",
                LastName = "testlast",
                PhoneNumber = 5555555555,
                Address = "1234 Main Street",
                City = "San Luis Obispo",
                State = "CA",
                Zip = "93401"
            };

            ContactsProviderMock.Setup(a => a.FetchContact(contact.Id))
                .Returns(contact);

            //when I fetch the client
            var httpResponse = await Client.GetAsync(Url + contact.Id);

            //then I expect the response 
            httpResponse.StatusCode.Should().Be(HttpStatusCode.OK);
            var response = await httpResponse.Content.ReadAsAsync<CreateContactResponse>();
            response.Id.Should().Be(contact.Id);
            response.FirstName.Should().Be(contact.FirstName);
            response.LastName.Should().Be(contact.LastName);
        }

        [Fact(DisplayName = "Create contact should return 400 for empty request")]
        public async Task Test2()
        {
            //given I have a test server and client
            Setup();

            var id = "FakeId";

            //when I fetch the client
            var httpResponse = await Client.GetAsync(Url + id);

            //then I expect the response 
            httpResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact(DisplayName = "Create contact should return 403 when required field is missing")]
        public async Task Test3()
        {
            //given I have a test server and client
            Setup();

            //when I fetch the client
            var httpResponse = await Client.GetAsync(Url);

            //then I expect the response 
            httpResponse.StatusCode.Should().Be(HttpStatusCode.MethodNotAllowed);
        }

        private Contact MapToContactFrom(CreateContactRequest request)
        {
            return new Contact
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                PhoneNumber = request.PhoneNumber,
                Address = request.Address,
                City = request.City,
                State = request.State,
                Zip = request.Zip,
            };
        }
    }
}
