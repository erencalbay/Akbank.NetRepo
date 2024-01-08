using Microsoft.AspNetCore.Mvc;
using static WebAPI.Models.BookOperations.CreateOneGame.CreateOneGameCommand;
using static WebAPI.Models.BookOperations.UpdateOneGame.UpdateOneGameCommand;
using WebAPI.Models.BookOperations.CreateOneGame;
using WebAPI.Models.BookOperations.GetGames;
using WebAPI.Models.BookOperations.GetOneGame;
using WebAPI.Models.BookOperations.UpdateOneGame;
using WebAPI.Models.GameOperations.CreateOneGame;
using WebAPI.Models.GameOperations.DeleteOneGame;
using WebAPI.Models.GameOperations.UpdateOneGame;
using WebAPI.Repositories;
using WebAPI.Services.Contracts;
using AutoMapper;
using WebAPI.Models.GenreOperations.GetGenres;
using WebAPI.Models.GenreOperations.GetOneGenre;
using FluentValidation;
using WebAPI.Models.GenreOperations.CreateGenre;
using WebAPI.Models.GenreOperations.UpdateGenre;
using WebAPI.Models.GenreOperations.DeleteGenre;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        private readonly RepositoryContext _context;
        private readonly ILoggerService _logger;
        private readonly IMapper _mapper;

        public GenresController(RepositoryContext context, ILoggerService logger, IMapper mapper = null)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAllGenres()
        {
            //viewmodel
            GetGenresQuery query = new GetGenresQuery(_context, _mapper);
            var result = query.Handle();

            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetOneGenre(int id)
        {
            //viewmodel and config
            GetOneGenreCommand command = new GetOneGenreCommand(_context, _mapper, _logger);
            command.GenreID = id;
            GetOneGenreCommandValidator validator = new GetOneGenreCommandValidator();
            validator.ValidateAndThrow(command);

            var result = command.Handle();

            return Ok(result); //200
        }

        [HttpPost]
        public IActionResult CreateOneGenre([FromBody] CreateGenreModel genre)
        {
            //viewmodel
            CreateGenreCommand command = new CreateGenreCommand(_context, _logger);
            command.Model = genre;
            //validator
            CreateGenreCommandValidator validator = new CreateGenreCommandValidator();
            validator.ValidateAndThrow(command);

            command.Handle();
            return StatusCode(201, genre); //201 
        }

        [HttpPut("{id:int}")]
        public IActionResult UpdateOneGenre(int id, [FromBody] UpdateGenreModel updatedGenre)
        {

            //viewmodel
            UpdateGenreCommand command = new UpdateGenreCommand(_context, _logger);
            command.GenreId = id;
            command.Model = updatedGenre;
            //validator
            UpdateGenreCommandValidator validator = new UpdateGenreCommandValidator();
            validator.Validate(command);

            command.Handle();

            _context.SaveChanges();
            return Ok(command.Model); //200

        }

        [HttpDelete("{id}")]
        public IActionResult DeleteOneGenre(int id)
        {
            //viewmodel and conf
            DeleteGenreCommand command = new DeleteGenreCommand(_context, _logger);
            command.GenreId = id;

            DeleteGenreCommandValidator validator = new DeleteGenreCommandValidator();
            validator.ValidateAndThrow(command);

            command.Handle();
            return NoContent(); //204
        }
    }
}
