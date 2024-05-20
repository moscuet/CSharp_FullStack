using Eshop.Core.src.Entity;
using Eshop.Core.src.RepositoryAbstraction;
using Eshop.WebApi.src.Data;
using Microsoft.EntityFrameworkCore;

namespace Eshop.WebApi.src.Repo
{
    public class AddressRepository : IAddressRepository
    {
        private readonly EshopDbContext _context;
        private readonly DbSet<Address> _addresses;

        public AddressRepository(EshopDbContext context)
        {
            _context = context;
            _addresses = _context.Addresses;
        }

        public async Task<Address> CreateAsync(Address address)
        {
            await _addresses.AddAsync(address);
            await _context.SaveChangesAsync();
            return address;
        }

        public async Task<bool> UpdateAsync(Address address)
        {
            var existingAddress = await _addresses.AsNoTracking().FirstOrDefaultAsync(a => a.Id == address.Id);
            if (existingAddress == null)
            {
                throw new KeyNotFoundException($"Address with ID {address.Id} not found.");
            }

            _context.Update(address);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Address> GetByIdAsync(Guid id)
        {
            var address = await _addresses.FirstOrDefaultAsync(a => a.Id == id);
            if (address == null)
            {
                throw new KeyNotFoundException($"Address with ID {id} not found.");
            }
            return address;
        }

        public async Task<bool> DeleteByIdAsync(Guid id)
        {
            var address = await _addresses.FirstOrDefaultAsync(a => a.Id == id);
            if (address == null)
            {
                throw new KeyNotFoundException($"Address with ID {id} not found.");
            }

            _addresses.Remove(address);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Address>> GetAllUserAddressesAsync(Guid userId)
        {
            return await _addresses.Where(a => a.UserId == userId).ToListAsync();
        }
    }
}
