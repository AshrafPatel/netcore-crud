using Contacts.DTO;
using Contacts.Services.Contacts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Contacts.Controllers.Contacts
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private readonly IContactService _contactService;
        private readonly ILogger<ContactsController> _logger;

        public ContactsController(IContactService contactService, ILogger<ContactsController> logger)
        {
            _contactService = contactService ?? throw new ArgumentNullException(nameof(contactService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        [ActionName("GetAllContactsAsync")]
        public async Task<IActionResult> GetAllContactsAsync()
        {
            return Ok(await _contactService.GetAllContacts());
        }

        [HttpGet]
        [ActionName("GetAContactAsync")]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetAContactAsync([FromRoute] Guid id)
        {
            var contact = await _contactService.GetContactAsync(id);
            if (contact == null) { return NotFound(); }
            return Ok(contact);
        }

        [HttpGet]
        [ActionName("GetAContactsByName")]
        [Route("{name}")]
        public async Task<IActionResult> GetContactsByName([FromRoute] string name)
        {
            var contacts = await _contactService.FindContactByName(name);
            if (contacts == null) { return NotFound();}
            return Ok(contacts);
        }

        [HttpPost]
        [ActionName("AddContactAsync")]
        public async Task<IActionResult> AddContactAsync([FromBody] ContactDto contactDto)
        {
            bool isCreated = await _contactService.AddAsync(contactDto);
            return CreatedAtAction(nameof(GetAContactAsync), contactDto);
        }

        [HttpPut]
        [ActionName("UpdateContactAsync")]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateContactAsync([FromRoute] Guid id, [FromBody] ContactDto contactDto)
        {
            var contactInDb = await _contactService.GetContactAsync(id);
            if (contactInDb == null) { return NotFound(); }

            contactInDb = await _contactService.UpdateAsync(id, contactDto);
            return Ok(contactInDb);
        }

        [HttpDelete]
        [ActionName("DeleteContactAsync")]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteContactAsync([FromRoute] Guid id)
        {
            var contactInDb = await _contactService.GetContactAsync(id);
            if (contactInDb == null) { return NotFound();  }

            await _contactService.DeleteAsync(id);
            return RedirectToAction(nameof(GetAllContactsAsync));
        }
    }
}
