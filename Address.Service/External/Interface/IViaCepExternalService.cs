using Address.Infrastructure.ExternalServices.Models;

namespace Address.Service.External.Interface
{
    public interface IViaCepExternalService
    {
        Task<Domain.Entities.Address> GetCepAsync(string cep);
    }
}
