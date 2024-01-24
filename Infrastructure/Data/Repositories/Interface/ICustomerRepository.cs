using Address.Domain.Entities;

namespace Address.Infrastructure.Data.Repositories.Interface
{
    public interface ICustomerRepository
    {
        Task CreateAsync(Customer customer);
        Task UpdateAsync(Customer customer);
        Task DeleteAsync(Guid customerId);
        Task<Customer> GetByIdAsync(Guid customerId);
    }
}
