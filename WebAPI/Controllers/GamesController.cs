using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using NLog;
using WebAPI.Models;
using WebAPI.Models.Exceptions;
using WebAPI.Repositories;
using WebAPI.Services.Contracts;

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
            var games = _context.Games.ToList();
            return Ok(games); //200
        }

        [HttpGet("GetOneGame")]
        public IActionResult GetOneGame([FromQuery] int id)
        {
            var game = _context
                .Games
                .Where(b => b.Id.Equals(id))
                .SingleOrDefault();

            if (game is null)
            {
                string message = $"The Game with id:{id} could not found.";
                _logger.LogError(message);

                //custom notfoundexception
                throw new NotFoundException(message);
            }

            return Ok(game); //200
        }

        [HttpPost]
        public IActionResult CreateOneGame([FromBody] Game game)
        {
            if (game is null)
                //custom badrequestexception
                throw new BadRequestException("Please fill the areas.");

            _context.Games.Add(game);
            _context.SaveChanges();
            return StatusCode(201, game); //201 
        }

        [HttpPut("{id:int}")]
        public IActionResult UpdateOneGame([FromRoute(Name = "id")] int id,
            [FromBody] Game game)
        {

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

            if (id != game.Id)
                //custom badrequestexception
                throw new BadRequestException("ID's do not match.");

            entity.Title = game.Title;
            entity.Price = game.Price;

            _context.SaveChanges();
            _logger.LogInfo($"The Game with id:{id} updated");
            return Ok(game); //200
        
        }

        [HttpDelete("{id:int}")]
        public IActionResult DeleteOneGame([FromRoute(Name = "id")] int id)
        {
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

            _context.Games.Remove(entity);
            _context.SaveChanges();

            _logger.LogInfo($"The Game with id:{id} deleted");
            return NoContent(); //204
        }

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
