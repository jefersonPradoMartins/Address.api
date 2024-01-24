using Address.Infrastructure.ExternalServices.Models;
using Newtonsoft.Json;
using Polly;
using System.Net;

namespace Address.Infrastructure.ExternalServices.Clients.ViaCep
{
    public class ViaCepRepository : IViaCepRepository
    {
        private readonly HttpClient _httpClient;

        private readonly IAsyncPolicy<HttpResponseMessage> _retryPolicy =
            Policy<HttpResponseMessage>.Handle<HttpRequestException>()
            .OrResult(x => x.StatusCode is >= HttpStatusCode.InternalServerError or
            HttpStatusCode.RequestTimeout)
            .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));

        public ViaCepRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ViaCepModel> GetDataAsync(string cep)
        {

            if (cep == null) throw new ArgumentNullException(nameof(cep));

            var response = await _retryPolicy.ExecuteAsync(() => _httpClient.GetAsync($"http://viacep.com.br/ws/{cep}/json"));
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            if(content.Contains("\"erro\": true"))
            {
                throw new Exception("cep não encontrado");
            }
            var result = JsonConvert.DeserializeObject<ViaCepModel>(content);

            return result;
        }
    }
}
