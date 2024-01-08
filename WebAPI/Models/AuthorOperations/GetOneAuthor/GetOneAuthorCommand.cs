using AutoMapper;
using WebAPI.Models.Exceptions;
using WebAPI.Repositories;
using WebAPI.Services.Contracts;

namespace WebAPI.Models.AuthorOperations.GetOneAuthor
{
    public class GetOneAuthorCommand
    {
        public int AuthorID { get; set; }

        private readonly RepositoryContext _context;
        private readonly ILoggerService _logger;
        private readonly IMapper _mapper;

        public GetOneAuthorCommand(RepositoryContext context, IMapper mapper, ILoggerService logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public AuthorOneViewModel Handle()
        {
            var author = _context.Author.Where(x => x.Id == AuthorID);
            if (author is null)
            {
                string message = $"The genre with id:{AuthorID} could not found.";
                _logger.LogError(message);
                throw new NotFoundException(message);
            }
            return _mapper.Map<AuthorOneViewModel>(author);
        }
    }

    public class AuthorOneViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime Birthdate { get; set; }
    }
}
