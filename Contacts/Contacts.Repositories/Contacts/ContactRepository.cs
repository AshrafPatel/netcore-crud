using Contacts.Data;
using Contacts.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Contacts.Services.Contacts
{
    public class ContactRepository : IContactRepository
    {
        private readonly ContactDbContext _contactDbContext;
        private readonly ILogger<ContactRepository> _logger;

        public ContactRepository(ContactDbContext contactDbContext, ILogger<ContactRepository> logger)
        {
            _contactDbContext = contactDbContext ?? throw new ArgumentNullException(nameof(contactDbContext));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<Contact> AddAsync(Contact contact)
        {
            contact.Id = Guid.NewGuid();
            await _contactDbContext.AddAsync(contact);
            await _contactDbContext.SaveChangesAsync();
            return contact;
        }

        public async Task DeleteAsync(Guid id)
        {
            var contact = _contactDbContext.Contacts.SingleOrDefault(x => x.Id == id);
            if (contact == null) { return; }
            _contactDbContext.Contacts.Remove(contact);
            await _contactDbContext.SaveChangesAsync();
        }

        public async Task<List<Contact>> GetAllContacts()
        {
            return await _contactDbContext.Contacts.ToListAsync();
        }

        public async Task<Contact> GetContactAsync(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentException($"Invalid contact ID {id}");
            }

            Contact? contact = await _contactDbContext.Contacts.SingleOrDefaultAsync(x => x.Id == id);

            if (contact == null)
            {
                throw new KeyNotFoundException("Contact not found");
            }
            return contact;
        }

        public async Task<Contact> UpdateAsync(Guid id, Contact contact)
        {
            var contactInDb = _contactDbContext.Contacts.SingleOrDefault(x =>x.Id == id);
            if (contactInDb == null)  throw new KeyNotFoundException($"Contact {id} not found");

            contactInDb.Name = contact.Name;
            contactInDb.Email = contact.Email;

            await _contactDbContext.SaveChangesAsync();
            return contactInDb;
        }

        public async Task<List<Contact>> FindContactByName(string name)
        {
            List<Contact> contacts = await _contactDbContext.Contacts.Where(x => x.Name.Contains(name)).ToListAsync();
            if (contacts == null) throw new KeyNotFoundException($"No contacts found with the given name {name}");
            return contacts;
        }
    }
}
