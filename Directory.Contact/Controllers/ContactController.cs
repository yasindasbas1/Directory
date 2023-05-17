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

    }
}
