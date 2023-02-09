using LibreriaVirtualApi.Data.Interfaces;
using LibreriaVirtualApi.Models;
using Microsoft.EntityFrameworkCore;

namespace LibreriaVirtualApi.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly BibliotecaVirtualContext _context;

        public UserRepository(BibliotecaVirtualContext context)
        {
            _context = context;
        }

        public async Task<User> Add(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public void Delete(User user)
        {
            _context.Users.Remove(user);
            _context.SaveChanges();
        }

        public async Task<bool> ExistUser(string email)
        {
            return await _context.Users.AnyAsync(u => u.Email == email);
        }

        public async Task<IEnumerable<User>> GetAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> GetByIdAsync(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
            return user!;
        }

        public async Task<User> Login(string email, string password)
        {
            var usuario = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (usuario is null)
                return null!;
            // TODO Verificar Password Cifrada
            return usuario!;
        }
    }
}
