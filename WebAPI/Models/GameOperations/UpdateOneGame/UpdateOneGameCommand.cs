using WebAPI.Models.Exceptions;
using WebAPI.Repositories;
using WebAPI.Services.Contracts;
using static WebAPI.Models.BookOperations.UpdateOneGame.UpdateOneGameCommand;

namespace WebAPI.Models.BookOperations.UpdateOneGame
{
    public class UpdateOneGameCommand
    {
        private readonly RepositoryContext _context;
        private readonly ILoggerService _logger;

        public UpdateOneGameModel Model { get; set; }
        public int GameId { get; set; }

        public UpdateOneGameCommand(RepositoryContext context, ILoggerService loggerService)
        {
            _context = context;
            _logger = loggerService;
        }

        public void Handle()
        {
            var game = _context.Games.Where(x => x.Id == GameId).SingleOrDefault();

            if (game is null)
            {
                string message = $"The Game with id:{GameId} could not found.";
                _logger.LogError(message);
                throw new NotFoundException($"The book with id:{GameId} not found");
            }

            game.Title = Model.Title;
            game.Price = Model.Price;

            _context.SaveChanges();
        }
        public class UpdateOneGameModel
        {
            public string Title { get; set; }
            public decimal Price { get; set; }
        }
    }
}
