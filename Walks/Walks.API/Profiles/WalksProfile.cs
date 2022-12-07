using AutoMapper;

namespace Walks.API.Profiles
{
    public class WalksProfile : Profile
    {
        public WalksProfile() 
        {
            CreateMap<Models.Domain.Walk, Models.DTO.WalkDto>()
                .ForMember(dto => dto.Id, options => options.MapFrom(domain => domain.Id))
                .ReverseMap();

            CreateMap<Models.Domain.WalkDifficulty, Models.DTO.WalkDifficultyDto>()
                .ForMember(dto => dto.Id, options => options.MapFrom(domain => domain.Id))
                .ReverseMap();
        }
    }
}
