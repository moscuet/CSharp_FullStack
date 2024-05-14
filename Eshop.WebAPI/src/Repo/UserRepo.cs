using AutoMapper;
using Eshop.Core.src.Common;
using Eshop.Core.src.Entity;
using Eshop.Core.src.RepoAbstraction;
using Eshop.WebApi.src.Data;
using Microsoft.EntityFrameworkCore;

namespace Eshop.WebApi.src.Repo
{
    public class UserRepo : IUserRepository
    {
        private readonly EshopDbContext _context;
        private readonly DbSet<User> _users;
        private readonly IMapper _mapper;

        public UserRepo(EshopDbContext context, IMapper mapper)
        {
            _context = context;
            _users = _context.Users;
            _mapper = mapper;
        }

        public async Task<User> CreateAsync(User user)
        {
            await _users.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }

            public async Task<bool> UpdateAsync(User user)
        {
            await _users
                .Where(u => u.Id == user.Id)
                .ExecuteUpdateAsync(setter =>
                    setter
                        .SetProperty(u => u.Email, user.Email)
                        .SetProperty(u => u.Avatar, user.Avatar)
                        .SetProperty(u => u.FirstName, user.FirstName)
                        .SetProperty(u => u.Password, user.Password)
                        .SetProperty(u => u.LastName, user.LastName)
                );
            await _context.SaveChangesAsync();
            return true;
        }



        public async Task<User>? GetByIdAsync(Guid id)
        {
            return await _users.SingleAsync(p => p.Id == id);
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
