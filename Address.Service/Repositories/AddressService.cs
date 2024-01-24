
using Address.Infrastructure.Data.Repositories.Interface;
using Address.Service.Repositories.Interface;


namespace Address.Service.Repositories
{
    public class AddressService : IAddressService
    {
        private readonly IAddressRepository _addressR;

        public AddressService(IAddressRepository addressRepository)
        {
            _addressR = addressRepository;
        }

        public async Task CreateAsync(Domain.Entities.Address address)
        {
            await _addressR.CreateAsync(address);
        }

        public async Task DeleteAsync(Guid addressId)
        {
            await _addressR.DeleteAsync(addressId);
        }

        public async Task<Domain.Entities.Address> GetByIdAsync(Guid addressId)
        {
            return await _addressR.GetByIdAsync(addressId);
        }

        public async Task UpdateAsync(Domain.Entities.Address address)
        {
            await _addressR.UpdateAsync(address);
        }
    }
}
