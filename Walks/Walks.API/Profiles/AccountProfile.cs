using AutoMapper;
using System.Runtime.InteropServices;

namespace Walks.API.Profiles
{
    public class AccountProfile : Profile
    {
        public AccountProfile() 
        {
            CreateMap<Models.Domain.User, Models.DTO.UserDto>()
                .ForMember(dto => dto.Username, options => options.MapFrom(domain => domain.UserName))
                .ReverseMap();
        }
    }
}
