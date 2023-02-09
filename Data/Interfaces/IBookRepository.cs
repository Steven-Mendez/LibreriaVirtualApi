using LibreriaVirtualApi.Models;

namespace LibreriaVirtualApi.Data.Interfaces
{
    public interface IBookRepository
    {
        Task<IEnumerable<Book>> GetAsync();
        Task<Book> GetByIdAsync(int id);
        Task<Book> Add(Book Book);
        void Update(Book Book);
        void Delete(Book Book);
        Task<bool> SaveAll();
    }
}
