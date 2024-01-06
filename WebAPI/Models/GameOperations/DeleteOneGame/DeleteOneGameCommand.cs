using WebAPI.Models.Exceptions;
using WebAPI.Repositories;
using WebAPI.Services.Contracts;

namespace WebAPI.Models.GameOperations.DeleteOneGame
{
    public class DeleteOneGameCommand
    {
        private readonly RepositoryContext _context;
        private readonly ILoggerService _logger;

        public int GameId { get; set; }
        public DeleteOneGameCommand(RepositoryContext context, ILoggerService logger)
        {
            _context = context;
            _logger = logger;
        }

        public void Handle()
        {
            var game = _context.Games.SingleOrDefault(x => x.Id == GameId);
            if (game is null)
            {
                string message = $"The Game with id:{GameId} could not found.";
                _logger.LogError(message);
                throw new NotFoundException($"The book with id:{GameId} not found");
            }

            _context.Games.Remove(game);
            _context.SaveChanges();
        }
    }
}
