using AutoMapper;
using LibreriaVirtualApi.Data.Interfaces;
using LibreriaVirtualApi.Dtos;
using LibreriaVirtualApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace LibreriaVirtualApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private IUserRepository _userRepository;
        private IBookRepository _repo;
        public IMapper _mapper;

        public BookController(IBookRepository repo, IUserRepository userRepository, IMapper mapper)
        {
            _repo = repo;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var books = await _repo.GetAsync();

            var booksDto = new List<BookResponseDto>();
            foreach (var book in books)
            {
                booksDto.Add(_mapper.Map<BookResponseDto>(book));
            }
            return Ok(booksDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var book = await _repo.GetByIdAsync(id);

            if (book is null)
                return NotFound("Libro no encontrado");

            var bookDto = _mapper.Map<BookResponseDto>(book);
            return Ok(bookDto);
        }

        [HttpPost]
        public async Task<IActionResult> Post(BookRequestDto bookDto)
        {
            if (bookDto.Quantity <= 0)
                return BadRequest("Cantidad No puede ser igual o menor que 0.");

            if (bookDto.Price <= 0)
                return BadRequest("Precio No puede ser igual o menor que 0.");

            if (await _userRepository.GetByIdAsync(bookDto.UserId!) is null)
                return BadRequest("No existe el usuario");

            var newBook = _mapper.Map<Book>(bookDto);
            await _repo.Add(newBook);

            if (!await _repo.SaveAll())
                return BadRequest();

            var response = _mapper.Map<BookResponseDto>(newBook);

            return Ok(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, BookRequestDto bookDto)
        {
            var bookInDb = await _repo.GetByIdAsync(id);

            if (bookInDb is null)
                return NotFound("El libro no existe");

            bookInDb = _mapper.Map(bookDto, bookInDb);

            _repo.Update(bookInDb);

            if (!await _repo.SaveAll())
                return NoContent();

            var response = _mapper.Map<BookResponseDto>(bookInDb);
            return Ok(response);
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

            var result = _mapper.Map<BookResponseDto>(bookInDb);
            return Ok(result);
        }
    }
}
