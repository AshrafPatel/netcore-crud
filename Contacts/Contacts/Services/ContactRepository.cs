using Contacts.Data;
using Contacts.Models;
using Microsoft.EntityFrameworkCore;

namespace Contacts.Services
{
    public class ContactRepository : IContactRepository
    {
        private readonly ContactDbContext _contactDbContext;

        public ContactRepository(ContactDbContext contactDbContext)
        {
            _contactDbContext = contactDbContext;
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

        public async Task<IEnumerable<Contact>> GetAllContacts()
        {
            return await _contactDbContext.Contacts.ToListAsync();
        }

        public async Task<Contact> GetContactAsync(Guid id)
        {
            return await _contactDbContext.Contacts.SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Contact> UpdateAsync(Guid id, Contact contact)
        {
            var contactInDb = _contactDbContext.Contacts.SingleOrDefault(x =>x.Id == id);
            if (contactInDb == null) { return null; }

            contactInDb.Name = contact.Name;
            contactInDb.Email = contact.Email;

            await _contactDbContext.SaveChangesAsync();
            return contactInDb;
        }

        public async Task<List<Contact>> FindContactByName(string name)
        {
            List<Contact> contacts = await _contactDbContext.Contacts.Where(x => x.Name.Contains(name)).ToListAsync();
            if (contacts == null) { return null; }
            return contacts;
        }
    }
}
