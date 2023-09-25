#nullable disable
using Assignment3.Data.Dtos.Franchises;
using Assignment3.Models;
using AutoMapper;
namespace Assignment3.Services.Mappers
{
    public class FranchiseProfile : Profile
    {
        public FranchiseProfile()
        {
            CreateMap<Franchise, FranchiseDto>()
                .ForMember(fdto => fdto.Movies,
                    opt => opt.MapFrom(f => f.Movies
                        .Select(f => f.Id).ToList()));

            CreateMap<FranchisePostDto, Franchise>();
        }
    }
}
