using Address.Infrastructure.ExternalServices.Models;


namespace Address.Infrastructure.ExternalServices.Clients.ViaCep
{
    public interface IViaCepRepository
    {
        Task<ViaCepModel> GetDataAsync(string cep);

    }
}
