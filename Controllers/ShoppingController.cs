using AutoMapper;
using LibreriaVirtualApi.Data.Interfaces;
using LibreriaVirtualApi.Dtos;
using LibreriaVirtualApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibreriaVirtualApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingController : ControllerBase
    {
        private readonly IShoppingCarRepository _shoppingCarRepository;
        private readonly IBookRepository _bookRepository;
        public IMapper _mapper;
        private IUserRepository _userRepository;

        public ShoppingController(IShoppingCarRepository shoppingCarRepository, IMapper mapper, IUserRepository userRepository, IBookRepository bookRepository)
        {
            _shoppingCarRepository = shoppingCarRepository;
            _mapper = mapper;
            _userRepository = userRepository;
            _bookRepository = bookRepository;
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> Get(int userId)
        {
            var userShoppingCar = await _shoppingCarRepository.GetByIdAsync(userId);

            return Ok(userShoppingCar);
        }

        [HttpPost]
        public async Task<IActionResult> Post(ShoppingCartRequestDto shoppinCarDto)
        {
            var book = await _bookRepository.GetByIdAsync(shoppinCarDto.BookId);
            var user = await _userRepository.GetByIdAsync(shoppinCarDto.UserId);

            if (book is null)
                return BadRequest();

            if (user is null)
                return BadRequest();

            var myShoppingCart = await _shoppingCarRepository.GetByIdAsync(shoppinCarDto.UserId);

            if (myShoppingCart.Any(sh => sh.BookId == shoppinCarDto.BookId))
                return BadRequest("Ya existe en la tabla");

            var newShoppingCartEntry = _mapper.Map<ShoppingCar>(shoppinCarDto);

            await _shoppingCarRepository.AddEntry(newShoppingCartEntry);

            if (!await _shoppingCarRepository.SaveAll())
                return BadRequest();

            var response = _mapper.Map<ShoppingCartResponseDto>(newShoppingCartEntry);

            return Ok(response);
        }

        [HttpPost("BuyAllEntries")]
        public async Task<IActionResult> BuyAllEntries(int userId)
        {
            await _shoppingCarRepository.BuyAllCar(userId);
            return Ok(await _shoppingCarRepository.SaveAll());
        }
    }
}
