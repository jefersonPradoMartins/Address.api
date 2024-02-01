namespace Address.Infrastructure.Data.Repositories.Interface
{
    public interface IAddressRepository
    {
        Task CreateAsync(Domain.Entities.Address address);
        Task UpdateAsync(Domain.Entities.Address address);
        Task DeleteAsync(Guid addressId);
        Task<Domain.Entities.Address> GetByIdAsync(Guid addressId);

        Task<IEnumerable<Domain.Entities.Address>> GetAllAsync();
    }
}
