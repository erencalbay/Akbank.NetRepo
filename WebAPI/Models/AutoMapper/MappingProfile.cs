using AutoMapper;
using WebAPI.Models.GenreOperations.GetGenres;
using WebAPI.Models.GenreOperations.GetOneGenre;

namespace WebAPI.Models.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Genres, GenresViewModel>();
            CreateMap<Genres, GenreOneGameViewModel>();
            CreateMap<GenreOneGameViewModel, Genres>();
        }
    }
}
