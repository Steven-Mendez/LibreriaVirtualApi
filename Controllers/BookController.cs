using LibreriaVirtualApi.Data.Interfaces;
using LibreriaVirtualApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibreriaVirtualApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private IUserRepository _userRepository;
        private IBookRepository _repo;

        public BookController(IBookRepository repo, IUserRepository userRepository)
        {
            _repo = repo;
            _userRepository = userRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var book = await _repo.GetAsync();
            return Ok(book);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var book = await _repo.GetByIdAsync(id);
            return Ok(book);
        }

        [HttpPost]
        public async Task<IActionResult> Post(Book book)
        {
            if (book.Quantity <= 0)
                return BadRequest("Cantidad No puede ser igual o menor que 0.");

            if (book.Price <= 0)
                return BadRequest("Precio No puede ser igual o menor que 0.");

            if (await _userRepository.GetByIdAsync((int)book.UserId!) is null)
                return BadRequest("No existe el usuario");

            await _repo.Add(book);

            if (await _repo.SaveAll())
                return Ok(book);

            return BadRequest();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Book book)
        {
            if (id != book.Id)
                BadRequest("Ids no coinciden");

            var bookInDb = await _repo.GetByIdAsync(id);

            if (bookInDb is null)
                return NotFound("El libro no existe");

            bookInDb.Author = book.Author;
            bookInDb.Quantity = book.Quantity;
            bookInDb.Edition = book.Edition;

            _repo.Update(bookInDb);

            if (await _repo.SaveAll())
                return Ok(bookInDb);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var bookInDb = await _repo.GetByIdAsync(id);

            if (bookInDb is null)
                return BadRequest("El libro no existe");

            _repo.Delete(bookInDb);

            if (!await _repo.SaveAll())
                return NoContent();

            return Ok(bookInDb);
        }
    }
}
