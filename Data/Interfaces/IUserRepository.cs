using LibreriaVirtualApi.Models;

namespace LibreriaVirtualApi.Data.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAsync();
        Task<User> GetByIdAsync(int id);
        Task<User> Add(User user);
        void Delete(User user);
        Task<User> Login(string email, string password);
        Task<bool> ExistUser(string email);
    }
}
