using Application.IUnitOfWork;
using AutoMapper;
using ContactAppBackend.Models.Dtos.ContactDto;
using ContactAppBackend.Models.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactAppBackend.Services.ContactServices
{
    public class ContactService : IContactService
    {
        private readonly IAppUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ContactService(IAppUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ContactDto>> GetAllContactsAsync()
        {
            var contactRepository = _unitOfWork.Repository<Contact>();
            var contacts = await contactRepository.GetAllAsync(asNoTracking: true);
            return _mapper.Map<IEnumerable<ContactDto>>(contacts);
        }

        public async Task<ContactDto?> GetContactByIdAsync(int id)
        {
            var contactRepository = _unitOfWork.Repository<Contact>();
            var contactEntity = (await contactRepository.FindAsync(c => c.Id == id, asNoTracking: true)).FirstOrDefault();

            if (contactEntity == null)            
                return null;
            
            return _mapper.Map<ContactDto>(contactEntity);
        }

        public async Task<ContactDto> CreateContactAsync(CreateContactDto createContactDto)
        {
            var contactEntity = _mapper.Map<Contact>(createContactDto);
            await _unitOfWork.Repository<Contact>().InsertAsync(contactEntity);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<ContactDto>(contactEntity);
        }

        public async Task<ContactDto?> UpdateContactAsync(UpdateContactDto updateContactDto)
        {
            var contactRepository = _unitOfWork.Repository<Contact>();
            var existingContact = (await contactRepository.FindAsync(c => c.Id == updateContactDto.Id)).FirstOrDefault();

            if (existingContact == null)
                return null;
            
            _mapper.Map(updateContactDto, existingContact);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<ContactDto>(existingContact);
        }

        public async Task<bool> DeleteContactAsync(int id)
        {
            var contactRepository = _unitOfWork.Repository<Contact>();
            var contactToDelete = (await contactRepository.FindAsync(c => c.Id == id)).FirstOrDefault();
            if (contactToDelete == null)
               return false;
            
            contactRepository.Remove(contactToDelete);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}