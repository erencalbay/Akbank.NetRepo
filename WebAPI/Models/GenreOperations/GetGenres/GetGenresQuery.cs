using AutoMapper;
using WebAPI.Repositories;

namespace WebAPI.Models.GenreOperations.GetGenres
{
    public class GetGenresQuery
    {
        private readonly RepositoryContext _context;
        private readonly IMapper _mapper;

        public GetGenresQuery(RepositoryContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<GenresViewModel> Handle()
        {
            var genres = _context.Genres.Where(x => x.IsActive).OrderBy(x => x.Id).ToList();
            List<GenresViewModel> returnObj = _mapper.Map<List<GenresViewModel>>(genres);
            return returnObj;
        }
    }

    public class GenresViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
