using Contacts.DTO;

namespace Contacts.UI.Services
{
    public class ContactsApiClient
    {
        private readonly HttpClient _http;

        public ContactsApiClient(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<ContactDto>> GetAllAsync()
        {
            return await _http.GetFromJsonAsync<List<ContactDto>>("https://localhost:5000/api/contacts");
        }
    }
}
