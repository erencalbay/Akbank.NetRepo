using AutoMapper;
using WebAPI.Repositories;

namespace WebAPI.Models.AuthorOperations.GetAuthors
{
    public class GetAuthorsQuery
    {
        private readonly RepositoryContext _context;
        private readonly IMapper _mapper;

        public GetAuthorsQuery(RepositoryContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<AuthorsViewModel> Handle()
        {
            var author = _context.Author.OrderBy(x => x.Id).ToList();
            List<AuthorsViewModel> returnObj = _mapper.Map<List<AuthorsViewModel>>(author);
            return returnObj;
        }
    }

    public class AuthorsViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime Birthdate { get; set; }
    }
}
