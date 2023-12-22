using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Models;
using WebAPI.Repositories;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GamesController : ControllerBase
    {
        private readonly RepositoryContext _context;

        public GamesController(RepositoryContext context)
        {
            _context = context;
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
                return NotFound(); // 404

            return Ok(game); //200
        }

        [HttpPost]
        public IActionResult CreateOneGame([FromBody] Game game)
        {
            try
            {
                if (game is null)
                    return BadRequest(); //400

                _context.Games.Add(game);
                _context.SaveChanges();
                return StatusCode(201, game); //201 
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message); //500
            }
        }

        [HttpPut("{id:int}")]
        public IActionResult UpdateOneGame([FromRoute(Name = "id")] int id,
            [FromBody] Game game)
        {

            try
            {
                var entity = _context
                .Games
                .Where(b => b.Id.Equals(id))
                .SingleOrDefault();

                if (entity is null)
                    return NotFound(); //404

                if (id != game.Id)
                    return BadRequest(); //400

                entity.Title = game.Title;
                entity.Price = game.Price;

                _context.SaveChanges();
                return Ok(game); //200
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        
        }

        [HttpDelete("{id:int}")]
        public IActionResult DeleteAllGames([FromRoute(Name = "id")] int id)
        {
            try
            {
                var entity = _context
                    .Games
                    .Where(b => b.Id.Equals(id))
                    .SingleOrDefault();

                if(entity is null)
                    return NotFound(new
                    {
                        statusCode = 404,
                        message = $"The game with id:{id} could not found."
                    }); //404

                _context.Games.Remove(entity);
                _context.SaveChanges();

                return NoContent(); //204
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message); //500
            }
        }

        [HttpPatch("{id:int}")]
        public IActionResult PartiallyUpdateOneGame([FromRoute(Name = "id")] int id,
            [FromBody] JsonPatchDocument<Game> gamePatch)
        {
            try
            {
                // check entity
                var entity = _context
                    .Games
                    .Where(b => b.Id.Equals(id))
                    .SingleOrDefault();

                if (entity is null)
                    return NotFound(); // 404

                gamePatch.ApplyTo(entity);
                _context.SaveChanges();

                return NoContent(); // 204
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
