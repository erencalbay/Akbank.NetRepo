using WebAPI.Models.Exceptions;
using WebAPI.Repositories;
using WebAPI.Services.Contracts;

namespace WebAPI.Models.AuthorOperations.UpdateAuthor
{
    public class UpdateAuthorCommand
    {
        public int AuthorId { get; set; }

        public UpdateAuthorModel Model { get; set; }
        public readonly RepositoryContext _context;
        public readonly ILoggerService _logger;

        public UpdateAuthorCommand(RepositoryContext context, ILoggerService logger)
        {
            _context = context;
            _logger = logger;
        }

        public void Handle()
        {
            var author = _context.Author.Where(x => x.Id == AuthorId).SingleOrDefault();

            if (author is null)
            {
                string message = $"The Author with id:{AuthorId} could not found.";
                _logger.LogError(message);
                throw new NotFoundException(message);
            }

            if (_context.Author.Any(x => x.Name.ToLower() == Model.Name.ToLower() && x.Surname.ToLower() == Model.Surname.ToLower() && x.Id != AuthorId))
            {
                throw new InvalidOperationException("The Author Name is exist");
            }
            author.Surname = string.IsNullOrEmpty(Model.Surname.Trim()) == default ? author.Surname : Model.Name;
            author.Name = string.IsNullOrEmpty(Model.Name.Trim()) == default ? author.Name : Model.Name;

            _context.SaveChanges();
        }

    }
    public class UpdateAuthorModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}
