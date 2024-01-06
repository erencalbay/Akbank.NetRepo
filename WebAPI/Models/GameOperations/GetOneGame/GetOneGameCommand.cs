using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Models.BookOperations.GetGames;
using WebAPI.Models.Exceptions;
using WebAPI.Repositories;
using WebAPI.Services.Contracts;

namespace WebAPI.Models.BookOperations.GetOneGame
{

    public class GetOneGameCommand
    {
        public GetOneGameModel Model { get; set; }
        public int GameId { get; set; }
        private readonly RepositoryContext _context;
        private readonly ILoggerService _logger;
        public GetOneGameCommand(RepositoryContext context, ILoggerService logger)
        {
            _context = context;
            _logger = logger;
        }

        public GetOneGameModel Handle()
        {
            var game = _context.Games.Where(x => x.Id == GameId).SingleOrDefault();
            
            if(game is null)
            {
                string message = $"The Game with id:{GameId} could not found.";
                _logger.LogError(message);
                throw new NotFoundException($"The book with id:{GameId} not found");
            }
            GetOneGameModel vm = new GetOneGameModel();
            vm.Title = game.Title;
            vm.Price = game.Price;
            return vm; //200
        }

        public class GetOneGameModel
        {
            public string Title { get; set; }
            public decimal Price { get; set; }
        }
    }
}
 