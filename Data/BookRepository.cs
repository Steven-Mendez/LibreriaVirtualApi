using LibreriaVirtualApi.Data.Interfaces;
using LibreriaVirtualApi.Models;
using Microsoft.EntityFrameworkCore;

namespace LibreriaVirtualApi.Data
{
    public class BookRepository : IBookRepository
    {
        private readonly BibliotecaVirtualContext _context;

        public BookRepository(BibliotecaVirtualContext context)
        {
            _context = context;
        }

        public async Task<Book> Add(Book book)
        {
            await _context.Books.AddAsync(book);
            return book;
        }

        public void Delete(Book Book)
        {
            _context.Books.Remove(Book);
        }

        public async Task<IEnumerable<Book>> GetAsync()
        {
            var books = await _context.Books.ToListAsync();
            return books;
        }

        public async Task<Book> GetByIdAsync(int id)
        {
            var book = await _context.Books.FirstOrDefaultAsync(u => u.Id == id);
            return book!;
        }

        public void Update(Book book)
        {
            _context.Update(book);
        }

        public async Task<bool> SaveAll()
        {
            var result = await _context.SaveChangesAsync() > 0;
            return result;
        }
    }
}
