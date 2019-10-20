using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNetCoreContactsAPI.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using DotNetCoreContactsAPI.Domain;
using DotNetCoreContactsAPI.Errors;


namespace DotNetCoreContactsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private readonly IContactsService _contactsService;

        public ContactsController(IContactsService contactsService)
        {
            _contactsService = contactsService;
        }

        /// <summary>
        /// Fetches a single contact by id
        /// </summary>
        /// <param name="request">Mandatory</param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        public ActionResult<GetContactResponse> GetContact(string id)
        {
            var result = _contactsService.FetchContact(id);

            if (result == null)
            {
                return NotFound();
            }

            return new GetContactResponse
            {
                Id = result.Id,
                FirstName = result.FirstName,
                LastName = result.LastName,
                PhoneNumber = result.PhoneNumber,
                Address = result.Address,
                City = result.City,
                State = result.State,
                Zip = result.Zip,
            };
        }

        /// <summary>
        /// Saves a single contact
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<CreateContactResponse>> CreateContact(
            [FromBody] CreateContactRequest request)
        {
            var contact = await _contactsService.Create(new Contact
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Address = request.Address,
                City = request.City,
                State = request.State,
                Zip = request.Zip,
                PhoneNumber = request.PhoneNumber
            });

            return new CreateContactResponse
            {
                Id = contact.Id,
                FirstName = contact.FirstName,
                LastName = contact.LastName,
                PhoneNumber = contact.PhoneNumber,
                Address = contact.Address,
                City = contact.City,
                State = contact.State,
                Zip = contact.Zip
            };
        }

        /// <summary>
        /// Deletes a contact
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{id}")]
        public ActionResult<DeleteContactResponse> DeleteContact(string id)
        {
            var result = _contactsService.DeleteContact(id);

            return new DeleteContactResponse
            {
                Success = result
            };

        }
    }
}