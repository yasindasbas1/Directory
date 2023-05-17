using Directory.Contact.Models;
using Directory.Contact.Services;
using Microsoft.AspNetCore.Mvc;

namespace Directory.Contact.Controllers
{
    [Route("api/Contact")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly ContactService _contactService;

        public ContactController(ContactService contactService)
        {
            _contactService = contactService;
        }

        [HttpGet("ContactSummary")]
        public async Task<IActionResult> GetContactSummary()
        {
            var vResult = await _contactService.GetContactSummary();
            return Ok(vResult);
        }

        [HttpGet("ContactSummaryById")]
        public async Task<IActionResult> GetContactSummaryById([FromBody] int contactId)
        {
            var vResult = await _contactService.GetContactSummaryById(contactId);
            return Ok(vResult);
        }

        [HttpPost("AddContact")]
        public async Task<IActionResult> AddContact(ContactInfo contactInfo)
        {
            var vResult = await _contactService.AddContact(contactInfo);
            return Ok(vResult);
        }

        [HttpPut("DeleteContact")]
        public async Task<IActionResult> DeleteContact([FromBody] int contactId)
        {
            var vResult = await _contactService.DeleteContact(contactId);
            return Ok(vResult);
        }

        [HttpPost("AddContactInformation")]
        public async Task<IActionResult> AddContactInformation(ContactInfo contactInfo)
        {
            var vResult = await _contactService.AddContactInformation(contactInfo);
            return Ok(vResult);
        }

    }
}
