using Microsoft.AspNetCore.Mvc;
using WebAPI.Models;
using WebAPI.Models.Exceptions;
using WebAPI.Repositories;
using WebAPI.Services.Contracts;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly RepositoryContext _context;
        private readonly ILoggerService _logger;

        public AuthController(RepositoryContext context, ILoggerService logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpPost]
        public IActionResult Login([FromBody] User user)
        {
            // user login control
            if (user.Username == "erencalbay" && user.Password == "Admin")
            {
                return Ok(); //200
            }
            throw new BadRequestException("Username or password is wrong");
        }
    }
}
