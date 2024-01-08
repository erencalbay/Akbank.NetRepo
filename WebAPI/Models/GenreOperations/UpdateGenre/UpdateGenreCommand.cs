using WebAPI.Models.Exceptions;
using WebAPI.Repositories;
using WebAPI.Services.Contracts;

namespace WebAPI.Models.GenreOperations.UpdateGenre
{
    public class UpdateGenreCommand
    {
        public int GenreId { get; set; }

        public UpdateGenreModel Model { get; set; }
        public readonly RepositoryContext _context;
        public readonly ILoggerService _logger;

        public UpdateGenreCommand(RepositoryContext context, ILoggerService logger)
        {
            _context = context;
            _logger = logger;
        }

        public void Handle()
        {
            var genre = _context.Genres.Where(x => x.Id == GenreId).SingleOrDefault();

            if (genre is null)
            {
                string message = $"The Genre with id:{GenreId} could not found.";
                _logger.LogError(message);
                throw new NotFoundException(message);
            }

            if(_context.Genres.Any(x => x.Name.ToLower() == Model.Name.ToLower() && x.Id != GenreId)) 
            {
                throw new InvalidOperationException("The Genre Name is exist");
            }

            genre.IsActive = Model.isActive;
            genre.Name = string.IsNullOrEmpty(Model.Name.Trim()) == default ? genre.Name : Model.Name  ;

            _context.SaveChanges();
        }

    }
    public class UpdateGenreModel
    {
        public string Name { get; set; }
        public bool isActive { get; set; } = true;
    }
}
