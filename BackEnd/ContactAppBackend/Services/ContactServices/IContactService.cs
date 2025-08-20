using ContactAppBackend.Models.Dtos.ContactDto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ContactAppBackend.Services.ContactServices
{
    public interface IContactService
    {
         Task<IEnumerable<ContactDto>> GetAllContactsAsync();
         Task<ContactDto?> GetContactByIdAsync(int id);

         Task<ContactDto> CreateContactAsync(CreateContactDto createContactDto);

         Task<ContactDto?> UpdateContactAsync(UpdateContactDto updateContactDto);

         Task<bool> DeleteContactAsync(int id);
    }
}