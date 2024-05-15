using Eshop.Core.src.Common;
using Eshop.Core.src.Entity;
using Eshop.Core.src.RepoAbstraction;
using Eshop.WebApi.src.Data;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace Eshop.WebApi.src.Repo
{
    public class UserRepo : IUserRepository
    {
        private readonly EshopDbContext _context;
        private readonly DbSet<User> _users;

        public UserRepo(EshopDbContext context)
        {
            _context = context;
            _users = _context.Users;
        }

        public async Task<User> CreateAsync(User user)
        {
            await _users.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<bool> UpdateAsync(User user)
        {
            var existingUser = await _users.AsNoTracking().FirstOrDefaultAsync(u => u.Id == user.Id);

            _context.Update(user);

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<User>? GetByIdAsync(Guid id)
        {
            return await _users.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<bool> DeleteByIdAsync(Guid id)
        {
            await _users.Where(p => p.Id == id).ExecuteDeleteAsync();
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<User> GetUserByCredentialAsync(UserCredential credential)
        {
            var foundUser = await _users.FirstOrDefaultAsync(user => user.Email == credential.Email && user.Password == credential.Password);
            return foundUser;
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync(QueryOptions options)
        {
            return await _users.ToListAsync();
        }

        public async Task<bool> UserExistsByEmailAsync(string email)
        {
            return await _users.AnyAsync(user => user.Email == email);
        }
    }
}
