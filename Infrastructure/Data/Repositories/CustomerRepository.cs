using Address.Domain.Entities;
using Address.Infrastructure.Data.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace Address.Infrastructure.Data.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly Context.Context _context;

        public CustomerRepository(Context.Context context)
        {
            _context = context;
        }

        public async Task CreateAsync(Customer customer)
        {
            try
            {
                await _context.Customer.AddAsync(customer);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {

            }
        }

        public async Task DeleteAsync(Guid customerId)
        {
            try
            {
                var result = await _context.Customer.AsNoTracking().FirstAsync(x => x.CustomerId == customerId);

                if (result != null)
                {
                    _context.Customer.Remove(result);
                    _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {

            }
        }

        public async Task<Customer> GetByIdAsync(Guid customerId)
        {
            try
            {
                var result = await _context.Customer.AsNoTracking().FirstAsync(x => x.CustomerId == customerId);
                return result;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task UpdateAsync(Customer customer)
        {
            try
            {
                _context.Customer.Update(customer);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

            }
        }
    }
}
