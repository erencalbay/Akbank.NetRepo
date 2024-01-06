using WebAPI.Models.Exceptions;
using WebAPI.Repositories;

namespace WebAPI.Models.BookOperations.CreateOneGame
{
    public class CreateOneGameCommand
    {
        public CreateOneGameModel Model { get; set; }

        private readonly RepositoryContext _context;

        public CreateOneGameCommand(RepositoryContext context)
        {
            _context = context;
        }

        public void Handle()
        {
            var game = _context.Games.FirstOrDefault(x => x.Title == Model.Title);

            if(game is not null)
            {
                throw new InvalidOperationException("Game is already exists ");
            }

            game = new Game();

            if (game is null)
                //custom badrequestexception
                throw new BadRequestException("Please fill the areas.");

            game.Title = Model.Title;
            game.Price = Model.Price;

            _context.Games.Add(game);
            _context.SaveChanges();
        }

        public class CreateOneGameModel
        {
            public string Title { get; set; }
            public decimal Price { get; set; }
        }
    }
}
