using Contacts.Models;

namespace Contacts.Services
{
    public interface IContactRepository
    {
        Task<Contact> AddAsync(Contact contact);
        Task DeleteAsync(Guid id);
        Task<IEnumerable<Contact>> GetAllContacts();
        Task<Contact> GetContactAsync(Guid id);
        Task<Contact> UpdateAsync(Guid id, Contact contact);
        Task<List<Contact>> FindContactByName(string name);
    }
}
