using AutoMapper;
using ContactAppBackend.Models.Dtos.ContactDto;
using ContactAppBackend.Models.Entities;

namespace ContactAppBackend.Models.Mappers
{
    public class ContactProfile : Profile
    {
        public ContactProfile()
        {
            CreateMap<CreateContactDto, Contact>();

            CreateMap<Contact, ContactDto>();

            CreateMap<UpdateContactDto, Contact>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}