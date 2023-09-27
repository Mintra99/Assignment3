#nullable disable
using Assignment3.Data.Dtos.Characters;
using Assignment3.Models;
using AutoMapper;

namespace Assignment3.Services.Mappers
{
    public class CharacterProfile : Profile
    {
        public CharacterProfile()
        { 
            CreateMap<Character,CharacterDto>().ForMember(fdto => fdto.Movies,
                    opt => opt.MapFrom(f => f.Movies
                        .Select(f => f.Id).ToList()));
            CreateMap<CharacterPostDTO, Character>();
            CreateMap<CharacterPutDTO, Character>();
        }
    }
}
