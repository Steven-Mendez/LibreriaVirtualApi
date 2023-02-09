using LibreriaVirtualApi.Data.Interfaces;
using LibreriaVirtualApi.Models;
using Microsoft.EntityFrameworkCore;

namespace LibreriaVirtualApi.Data
{
    public class ShoppingCarRepository : IShoppingCarRepository
    {
        private readonly BibliotecaVirtualContext _context;

        public ShoppingCarRepository(BibliotecaVirtualContext context)
        {
            _context = context;
        }

        public async Task<ShoppingCar> AddEntry(ShoppingCar shoppingCar)
        {
            await _context.ShoppingCars.AddAsync(shoppingCar);
            return shoppingCar;
        }

        public async Task BuyAllCar(int userId)
        {
            var userShoppingCar = _context.ShoppingCars.Where(sp => sp.UserId == userId).ToList();
            var sale = new SalesHistory()
            {
                CustomerId = userId,
            };
            await _context.SalesHistories.AddAsync(sale);
            foreach (var shoppinCarEntry in userShoppingCar)
            {
                var book =  _context.Books.FirstOrDefault(book => book.Id == shoppinCarEntry.BookId);
                book!.Quantity = book.Quantity - shoppinCarEntry.Quantity;
                var saleDetail = new SalesDetail()
                {
                    SaleId = sale.SaleId,
                    BookId = book.Id,
                    Price = book.Price,
                    BookQuantity = shoppinCarEntry.Quantity
                };
                _context.Books.Update(book);
                _context.SalesDetails.Add(saleDetail);
            }
            foreach (var shoppinCarEntry in userShoppingCar)
            {
                _context.ShoppingCars.Remove(shoppinCarEntry);
            }
        }

        public async void BuySingleBook(ShoppingCar shoppingCarEntry, int quantity)
        {
            var book = await _context.Books.FindAsync(shoppingCarEntry.BookId);
            book!.Quantity = book.Quantity - quantity;
            var sale = new SalesHistory()
            {
                CustomerId = shoppingCarEntry.UserId,
            };
            var saleDetail = new SalesDetail()
            {
                SaleId = sale.SaleId,
                BookId = book.Id,
                Price = book.Price,
                BookQuantity = shoppingCarEntry.Quantity
            };
            await _context.SalesDetails.AddAsync(saleDetail);
            _context.ShoppingCars.Remove(shoppingCarEntry);
        }

        public async void ClearCar(User user)
        {
            var userCarEntries = await _context.ShoppingCars.Where(sc => sc.UserId == user.Id).ToListAsync();
            _context.ShoppingCars.RemoveRange(userCarEntries);
        }

        public void DeleteEntry(ShoppingCar shoppingCar)
        {
            _context.ShoppingCars.Remove(shoppingCar!);
        }

        public async Task<IEnumerable<ShoppingCar>> GetByIdAsync(int userId)
        {
            var sp = await _context.ShoppingCars
                .Include(sc => sc.Book)
                .Where(sc => sc.UserId == userId).ToListAsync();
            return sp;
        }

        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public void UpdateEntry(ShoppingCar shoppingCar)
        {
            _context.ShoppingCars.Update(shoppingCar);
        }
    }
}
