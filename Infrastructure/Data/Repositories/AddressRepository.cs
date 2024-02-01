using Address.Infrastructure.Data.Context;
using Address.Infrastructure.Data.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Address.Infrastructure.Data.Repositories
{
    public class AddressRepository : IAddressRepository
    {
        private readonly Context.Context _context;


        public AddressRepository(Context.Context context)
        {
            _context = context;
        }
        public async Task CreateAsync(Domain.Entities.Address address)
        {
            await _context.Addresses.AddAsync(address);
            _context.SaveChanges();
        }
        public async Task DeleteAsync(Guid addressId)
        {
            var result = await _context.Addresses.AsNoTracking().FirstAsync(x => x.AddressId == addressId);

            if (result != null)
            {
                _context.Addresses.Remove(result);
                _context.SaveChanges();
            }
        }
        public async Task<IEnumerable<Domain.Entities.Address>> GetAllAsync()
        {
            var result = _context.Addresses.ToList();
            return result;
        }
        public async Task<Domain.Entities.Address> GetByIdAsync(Guid addressId)
        {
            var result = await _context.Addresses.AsNoTracking().FirstAsync(x => x.AddressId == addressId);
            return result;
        }
        public async Task UpdateAsync(Domain.Entities.Address address)
        {
            _context.Addresses.Update(address);
            await _context.SaveChangesAsync();
        }
    }
}
