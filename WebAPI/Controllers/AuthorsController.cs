using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Models.AuthorOperations.CreateAuthor;
using WebAPI.Models.AuthorOperations.DeleteAuthor;
using WebAPI.Models.AuthorOperations.GetAuthors;
using WebAPI.Models.AuthorOperations.GetOneAuthor;
using WebAPI.Models.AuthorOperations.UpdateAuthor;
using WebAPI.Repositories;
using WebAPI.Services.Contracts;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly RepositoryContext _context;
        private readonly ILoggerService _logger;
        private readonly IMapper _mapper;

        public AuthorsController(RepositoryContext context, ILoggerService logger, IMapper mapper = null)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAllAuthors()
        {
            //viewmodel
            GetAuthorsQuery query = new GetAuthorsQuery(_context, _mapper);
            var result = query.Handle();

            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetOneAuthor(int id)
        {
            //viewmodel and config
            GetOneAuthorCommand command = new GetOneAuthorCommand(_context, _mapper, _logger);
            command.AuthorID = id;
            GetOneAuthorCommandValidator validator = new GetOneAuthorCommandValidator();
            validator.ValidateAndThrow(command);

            var result = command.Handle();

            return Ok(result); //200
        }

        [HttpPost]
        public IActionResult CreateOneAuthor([FromBody] CreateAuthorModel Author)
        {
            //viewmodel
            CreateAuthorCommand command = new CreateAuthorCommand(_context, _logger);
            command.Model = Author;
            //validator
            CreateAuthorCommandValidator validator = new CreateAuthorCommandValidator();
            validator.ValidateAndThrow(command);

            command.Handle();
            return StatusCode(201, Author); //201 
        }

        [HttpPut("{id:int}")]
        public IActionResult UpdateOneAuthor(int id, [FromBody] UpdateAuthorModel updatedAuthor)
        {

            //viewmodel
            UpdateAuthorCommand command = new UpdateAuthorCommand(_context, _logger);
            command.AuthorId = id;
            command.Model = updatedAuthor;
            //validator
            UpdateAuthorCommandValidator validator = new UpdateAuthorCommandValidator();
            validator.Validate(command);

            command.Handle();

            _context.SaveChanges();
            return Ok(command.Model); //200

        }

        [HttpDelete("{id}")]
        public IActionResult DeleteOneAuthor(int id)
        {
            //viewmodel and conf
            DeleteAuthorCommand command = new DeleteAuthorCommand(_context, _logger);
            command.AuthorId = id;

            DeleteAuthorCommandValidator validator = new DeleteAuthorCommandValidator();
            validator.ValidateAndThrow(command);

            command.Handle();
            return NoContent(); //204
        }
    }
}
