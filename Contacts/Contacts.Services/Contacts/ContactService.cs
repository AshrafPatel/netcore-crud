using AutoMapper;
using Contacts.Data.Models;
using Contacts.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.Extensions.Logging;

namespace Contacts.Services.Contacts
{
    public class ContactService : IContactService
    {
        private readonly IContactRepository _contactRepository;
        private readonly ILogger<ContactService> _logger;
        private readonly IMapper _mapper;

        public ContactService(IContactRepository contactRepository, ILogger<ContactService> logger, IMapper mapper)
        {
            _contactRepository = contactRepository ?? throw new ArgumentNullException(nameof(contactRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<bool> AddAsync(ContactDto contactDto)
        {
            if (contactDto == null) return false;

            Contact contactTToAdd = _mapper.Map<Contact>(contactDto);

            await _contactRepository.AddAsync(contactTToAdd);
            return true;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            if (id == Guid.Empty) return false;

            Contact contact = await _contactRepository.GetContactAsync(id);
            if (contact == null) return false;

            await _contactRepository.DeleteAsync(contact.Id);
            return true;
        }

        public async Task<List<ContactDto>> FindContactByName(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) return new List<ContactDto>();

            List<Contact> contacts = await _contactRepository.FindContactByName(name);
            if (contacts == null || contacts.Count == 0) return new List<ContactDto>();
            
            List<ContactDto> contactDtos = _mapper.Map<List<ContactDto>>(contacts);
            return contactDtos;
        }

        public async Task<List<ContactDto>> GetAllContacts()
        {
            List<Contact> contacts = await _contactRepository.GetAllContacts();
            
            if (contacts == null || contacts.Count == 0) return new List<ContactDto>();
            
            List<ContactDto> contactDtos = _mapper.Map<List<ContactDto>>(contacts);
            return contactDtos;
        }

        public async Task<ContactDto> GetContactAsync(Guid id)
        {
            if (id == Guid.Empty) throw new NullReferenceException();

            Contact contact = await _contactRepository.GetContactAsync(id);
            if (contact == null) throw new KeyNotFoundException($"Contact with id {id} not found.");

            ContactDto contactDto = _mapper.Map<ContactDto>(contact);
            return contactDto;
        }

        public async Task<ContactDto> UpdateAsync(Guid id, ContactDto contactDto)
        {
            if (id == Guid.Empty || contactDto == null) throw new NullReferenceException();    

            Contact contact = _mapper.Map<Contact>(contactDto);
            Contact updatedContact = await _contactRepository.UpdateAsync(id, contact);

            ContactDto updateContactDto = _mapper.Map<ContactDto>(updatedContact);
            return updateContactDto;
        }
    }
}
