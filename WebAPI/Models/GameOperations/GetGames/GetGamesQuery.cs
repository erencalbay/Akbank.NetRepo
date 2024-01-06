using WebAPI.Models;
using WebAPI.Repositories;

namespace WebAPI.Models.BookOperations.GetGames
{
    public class GetGamesQuery
    {
        private readonly RepositoryContext _context;

        public GetGamesQuery(RepositoryContext context)
        {
            _context = context;
        }

        public List<Game> Handle()
        {
            var games = _context.Games.ToList();
            List<GameViewModel> vm = new List<GameViewModel>();
            foreach (var game in games)
            {
                vm.Add(new GameViewModel()
                {
                    Title = game.Title,
                    Price = game.Price,
                });
            }
            return games; //200
        }
    }

    public class GameViewModel
    {
        public string Title { get; set; }
        public decimal Price { get; set; }
    }
}
