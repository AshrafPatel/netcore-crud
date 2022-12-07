using AutoMapper;

namespace Walks.API.Profiles
{
    public class RegionsProfile : Profile
    {
        public RegionsProfile()
        {
            CreateMap<Models.Domain.Region, Models.DTO.RegionDto>()
                .ForMember(dto => dto.Id, options => options.MapFrom(domain => domain.Id))
                .ReverseMap();
        }
    }
}
