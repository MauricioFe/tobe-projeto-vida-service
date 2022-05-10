using FluentResults;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using tobeApi.Models;
using tobeApi.Services;

namespace tobeApi.Controllers
{
    [ApiController]
    public class ContactsControllers : ControllerBase
    {
        private readonly ContactsService _contactsService;

        public ContactsControllers(ContactsService contactsService)
        {
            this._contactsService = contactsService;
        }

        [HttpPost("contacts")]
        public IActionResult SendEmailContacts([FromBody] Contacts contacts)
        {
            Result result = _contactsService.SendEmailTobe(contacts);
            if (result.IsFailed) return StatusCode(500);
            return Ok(result.Successes);
        }
    }
}
