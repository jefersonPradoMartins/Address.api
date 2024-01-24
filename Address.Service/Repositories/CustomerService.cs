using Address.Domain.Entities;
using Address.Infrastructure.Data.Repositories.Interface;
using Address.Service.Repositories.Interface;


namespace Address.Service.Repositories
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerR;

        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerR = customerRepository;
        }

        public async Task CreateAsync(Customer customer)
        {
            await _customerR.CreateAsync(customer);
        }

        public async Task DeleteAsync(Guid customerId)
        {
            await _customerR.DeleteAsync(customerId);
        }

        public async Task<Customer> GetByIdAsync(Guid customerId)
        {
            return await _customerR.GetByIdAsync(customerId);
        }

        public async Task UpdateAsync(Customer customer)
        {
            await _customerR.UpdateAsync(customer);
        }
    }
}
