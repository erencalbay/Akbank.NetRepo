using WebAPI.Models.Exceptions;
using WebAPI.Repositories;
using WebAPI.Services.Contracts;

namespace WebAPI.Models.GenreOperations.DeleteGenre
{
    public class DeleteGenreCommand
    {
        public int GenreId { get; set; }

        public readonly RepositoryContext _context;
        public readonly ILoggerService _logger;

        public DeleteGenreCommand(RepositoryContext context, ILoggerService logger)
        {
            _context = context;
            _logger = logger;
        }

        public void Handle()
        {
            var genre = _context.Genres.SingleOrDefault(x => x.Id == GenreId);
            if (genre is null)
            {
                string message = $"The Genre with id:{GenreId} could not found.";
                _logger.LogError(message);
                throw new NotFoundException(message);
            }

            _context.Genres.Remove(genre);
            _context.SaveChanges();
        }
    }
}
