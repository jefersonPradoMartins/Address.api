using Address.Infrastructure.ExternalServices.Clients.ViaCep;
using Address.Service.External.Interface;

namespace Address.Service.External
{
    public class ViaCepExternalService : IViaCepExternalService
    {
        private readonly IViaCepRepository _viaCepR;

        public ViaCepExternalService(IViaCepRepository viaCepRepository)
        {
            _viaCepR = viaCepRepository;
        }

        public async Task<Domain.Entities.Address> GetCepAsync(string cep)
        {

            var result = await _viaCepR.GetDataAsync(cep);
            if (result == null) throw new HttpRequestException();

            return result.ToAddress();
        }
    }
}
