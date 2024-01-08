using AutoMapper;
using WebAPI.Models.Exceptions;
using WebAPI.Repositories;
using WebAPI.Services.Contracts;

namespace WebAPI.Models.GenreOperations.GetOneGenre
{
    public class GetOneGenreCommand
    {
        public int GenreID { get; set; }

        private readonly RepositoryContext _context;
        private readonly ILoggerService _logger;
        private readonly IMapper _mapper;

        public GetOneGenreCommand(RepositoryContext context, IMapper mapper, ILoggerService logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public GenreOneGameViewModel Handle()
        {
            var genre = _context.Genres.Where(x => x.IsActive && x.Id == GenreID);
            if (genre is null)
            {
                string message = $"The genre with id:{GenreID} could not found.";
                _logger.LogError(message);
                throw new NotFoundException($"The genre with id:{GenreID} not found");
            }
            return _mapper.Map<GenreOneGameViewModel>(genre);
        }
    }

    public class GenreOneGameViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
