using WebAPI.Models.Exceptions;
using WebAPI.Repositories;
using WebAPI.Services.Contracts;

namespace WebAPI.Models.AuthorOperations.CreateAuthor
{
    public class CreateAuthorCommand
    {
        public CreateAuthorModel Model;
        private readonly RepositoryContext _context;
        private readonly ILoggerService _logger;
        public CreateAuthorCommand(RepositoryContext context, ILoggerService logger)
        {
            _context = context;
            _logger = logger;
        }

        public void Handle()
        {
            var author = _context.Author.SingleOrDefault(x => x.Name == Model.Name && x.Surname == Model.Surname && x.Birthdate == Model.Birthdate);
            if (author is not null)
            {
                string message = "Author is already exists";
                _logger.LogError(message);
                throw new InvalidOperationException(message);
            }

            author = new Author();
            author.Name = Model.Name;
            author.Surname = Model.Surname;
            author.Birthdate = Model.Birthdate;

            _context.Author.Add(author);
            _context.SaveChanges();
        }
    }

    public class CreateAuthorModel
    {
        public string Name { get; set; }
        public string Surname { get; set;}
        public DateTime Birthdate { get; set;}
    }
}
