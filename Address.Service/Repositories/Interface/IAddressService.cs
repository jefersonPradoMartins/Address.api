using Address.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Address.Service.Repositories.Interface
{
    public interface IAddressService
    {
        Task CreateAsync(Domain.Entities.Address address);
        Task UpdateAsync(Domain.Entities.Address address);
        Task DeleteAsync(Guid addressId);
        Task<Domain.Entities.Address> GetByIdAsync(Guid addressId);
    }
}
