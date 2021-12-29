using System.Linq;
using API.DTOs;
using API.Entities;
using API.Extensions;
using AutoMapper;

namespace API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<TbAddUser, MemberDto>()
                .ForMember(f => f.PhotoUrl, opt => opt.MapFrom((src => src.Photos.FirstOrDefault(x => x.IsMain).Url))
                ).ForMember(dest => dest.Age, opt => opt.MapFrom(src => src.DateBirth.CalculateAge()));
            CreateMap<TbPhoto, PhotoDto>();
        }
    }
}