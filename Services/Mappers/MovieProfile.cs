#nullable disable
using Assignment3.Data.Dtos.Franchises;
using Assignment3.Data.Dtos.Movies;
using Assignment3.Models;
using AutoMapper;
namespace Assignment3.Services.Mappers
{
    public class MovieProfile : Profile
    {
        public MovieProfile()
        {
            CreateMap<Movie, MovieDto>()
                .ForMember(mdto => mdto.Characters,
                    opt => opt.MapFrom(m => m.Characters
                        .Select(m => m.Id).ToList()));

            CreateMap<MoviePostDto, Movie>();
            CreateMap<MoviePutDto, Movie>();
        }
    }
}
