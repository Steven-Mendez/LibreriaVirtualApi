using LibreriaVirtualApi.Data.Interfaces;
using LibreriaVirtualApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibreriaVirtualApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _repo;

        public UserController(IUserRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var users = await _repo.GetAsync();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var user = await _repo.GetByIdAsync(id);
            return Ok(user);
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register(User user)
        {
            user.Email = user.Email.ToLower();
            if (await _repo.ExistUser(user.Email))
                return BadRequest("El usuario ya se encuentra registradp");

            var createdUser = await _repo.Add(user);

            return Ok(createdUser);
        }
    }
}
