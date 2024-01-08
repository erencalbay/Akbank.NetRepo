using AutoMapper;
using WebAPI.Models.AuthorOperations.GetAuthors;
using WebAPI.Models.AuthorOperations.GetOneAuthor;
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
            CreateMap<Author, AuthorsViewModel>();
            CreateMap<Author, AuthorOneViewModel>();
            CreateMap<AuthorOneViewModel, Author>();
        }
    }
}
