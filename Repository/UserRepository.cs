using Apps.Data;
using Apps.Dtos.Users;
using Apps.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Apps.Repository
{
    public class UserRepository
    {
        private readonly DatabaseContext _context;

        public UserRepository(DatabaseContext context) { 
            _context = context;
        }

        public async Task<List<User>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> GetUserById(int Id)
        {
            return await _context.Users.FindAsync(Id);
        }

        public async Task StoreUser(CreateUserDto data)
        {
            User User = new()
            {
                Name = data.Name,
                Email = data.Email,
                Phone = data.Phone,
                Address = data.Address,
            };
            var password = new PasswordHasher<User>().HashPassword(User, data.Password);
            User.Password = password;

            await _context.Users.AddAsync(User);
            await _context.SaveChangesAsync();
        }

        public async Task<User> GetUserByEmail(string email)
        {
            return await _context.Users.FirstAsync(x => x.Email == email);
        }
    }
}
