using WebAPI.Models.Exceptions;
using WebAPI.Repositories;
using WebAPI.Services.Contracts;

namespace WebAPI.Models.AuthorOperations.DeleteAuthor
{
    public class DeleteAuthorCommand
    {
        public int AuthorId { get; set; }

        public readonly RepositoryContext _context;
        public readonly ILoggerService _logger;

        public DeleteAuthorCommand(RepositoryContext context, ILoggerService logger)
        {
            _context = context;
            _logger = logger;
        }

        public void Handle()
        {
            var author = _context.Author.SingleOrDefault(x => x.Id == AuthorId);
            if (author is null)
            {
                string message = $"The Author with id:{AuthorId} could not found.";
                _logger.LogError(message);
                throw new NotFoundException(message);
            }

            _context.Author.Remove(author);
            _context.SaveChanges();
        }
    }
}
