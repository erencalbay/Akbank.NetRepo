using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using NLog;
using WebAPI.Models;
using WebAPI.Models.BookOperations.CreateOneGame;
using WebAPI.Models.BookOperations.GetGames;
using WebAPI.Models.BookOperations.GetOneGame;
using WebAPI.Models.BookOperations.UpdateOneGame;
using WebAPI.Models.Exceptions;
using WebAPI.Models.GameOperations.DeleteOneGame;
using WebAPI.Repositories;
using WebAPI.Services.Contracts;
using static WebAPI.Models.BookOperations.CreateOneGame.CreateOneGameCommand;
using static WebAPI.Models.BookOperations.GetOneGame.GetOneGameCommand;
using static WebAPI.Models.BookOperations.UpdateOneGame.UpdateOneGameCommand;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GamesController : ControllerBase
    {
        private readonly RepositoryContext _context;
        private readonly ILoggerService _logger;

        public GamesController(RepositoryContext context, ILoggerService logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult GetAllGames()
        {
            //vm
            GetGamesQuery query = new GetGamesQuery(_context);
            var result = query.Handle();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetOneGame(int id)
        {
            GetOneGameCommand command = new GetOneGameCommand(_context, _logger);
            command.GameId = id;            
            var result = command.Handle();
            return Ok(result); //200
        }

        [HttpPost]
        public IActionResult CreateOneGame([FromBody] CreateOneGameModel game)
        {
            CreateOneGameCommand command = new CreateOneGameCommand(_context);
            command.Model = game;
            command.Handle();
            return StatusCode(201, game); //201 
        }

        [HttpPut("{id:int}")]
        public IActionResult UpdateOneGame(int id, [FromBody] UpdateOneGameModel updatedGame)
        {
            UpdateOneGameCommand command = new UpdateOneGameCommand(_context, _logger);
            command.GameId = id;
            command.Model = updatedGame;
            command.Handle();

            _context.SaveChanges();
            return Ok(command.Model); //200
        
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteOneGame(int id)
        {
            DeleteOneGameCommand command = new DeleteOneGameCommand(_context, _logger);
            command.GameId = id;
            command.Handle();
            return NoContent(); //204
        }

        //todo: vm 
        [HttpPatch("{id:int}")]
        public IActionResult PartiallyUpdateOneGame([FromRoute(Name = "id")] int id,
            [FromBody] JsonPatchDocument<Game> gamePatch)
        {

            // check entity
            var entity = _context
                .Games
                .Where(b => b.Id.Equals(id))
                .SingleOrDefault();

            if (entity is null)
            {
                string message = $"The Game with id:{id} could not found.";
                _logger.LogError(message);

                //custom notfoundexception
                throw new NotFoundException(message);
            }

            gamePatch.ApplyTo(entity);
            _context.SaveChanges();

            return NoContent(); // 204

        }
        /*
        [HttpGet("List")]
        public IActionResult ListProducts([FromQuery] string title)
        {
            try
            {
                var filteredEntity = _context.Games.Where(b => b.Title.Contains(title)).ToList();

                if (filteredEntity is null)
                    return NotFound();//404

                return Ok(filteredEntity);//200
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
            
        }*/
    }
}
