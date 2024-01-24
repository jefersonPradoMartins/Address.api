using Address.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Address.Service.Repositories.Interface
{
    public interface ICustomerService
    {
        Task CreateAsync(Customer customer);
        Task UpdateAsync(Customer customer);
        Task DeleteAsync(Guid customerId);
        Task<Domain.Entities.Customer> GetByIdAsync(Guid customerId);

    }
}
