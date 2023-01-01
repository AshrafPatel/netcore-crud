using Contacts.Models;
using Contacts.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Contacts.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private readonly IContactRepository _contactRepository;

        public ContactsController(IContactRepository contactRepository)
        {
            _contactRepository = contactRepository;
        }

        [HttpGet]
        [ActionName("GetAllContactsAsync")]
        public async Task<IActionResult> GetAllContactsAsync()
        {
            return Ok(await _contactRepository.GetAllContacts());
        }

        [HttpGet]
        [ActionName("GetAContactAsync")]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetAContactAsync([FromRoute] Guid id)
        {
            var contact = await _contactRepository.GetContactAsync(id);
            if (contact == null) { return NotFound(); }
            return Ok(contact);
        }

        [HttpGet]
        [ActionName("GetAContactsByName")]
        [Route("{name}")]
        public async Task<IActionResult> GetContactsByName([FromRoute] string name)
        {
            var contacts = await _contactRepository.FindContactByName(name);
            if (contacts == null) { return NotFound();}
            return Ok(contacts);
        }

        [HttpPost]
        [ActionName("AddContactAsync")]
        public async Task<IActionResult> AddContactAsync([FromBody] Contact contact)
        {
            var contactGoingToDb = new Contact()
            {
                Name = contact.Name,
                Email = contact.Email
            };
            contactGoingToDb = await _contactRepository.AddAsync(contactGoingToDb);
            return CreatedAtAction(nameof(GetAContactAsync), new { id = contactGoingToDb.Id }, contactGoingToDb);
        }

        [HttpPut]
        [ActionName("UpdateContactAsync")]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateContactAsync([FromRoute] Guid id, [FromBody] Contact contact)
        {
            var contactInDb = await _contactRepository.GetContactAsync(id);
            if (contactInDb == null) { return NotFound(); }

            contactInDb = await _contactRepository.UpdateAsync(id, contact);
            return Ok(contactInDb);
        }

        [HttpDelete]
        [ActionName("DeleteContactAsync")]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteContactAsync([FromRoute] Guid id)
        {
            var contactInDb = await _contactRepository.GetContactAsync(id);
            if (contactInDb == null) { return NotFound();  }

            await _contactRepository.DeleteAsync(id);
            return RedirectToAction(nameof(GetAllContactsAsync));
        }
    }
}
