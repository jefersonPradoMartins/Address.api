
namespace Address.Infrastructure.ExternalServices.Models
{
    public class ViaCepModel
    {
        public string cep { get; set; }
        public string logradouro { get; set; }
        public string complemento { get; set; }
        public string bairro { get; set; }
        public string localidade { get; set; }
        public string uf { get; set; }
        public string ibge { get; set; }
        public string gia { get; set; }
        public string ddd { get; set; }
        public string siafi { get; set; }



        public Domain.Entities.Address ToAddress()
        {
            var nAddress = new Domain.Entities.Address()
            {
                ZipCode = this.cep,
                Street = this.logradouro,
                Complements = this.complemento,
                Neighborhood = this.bairro,
                City = this.localidade,
                State = this.uf,
                Ibge = this.ibge,
            };

            return nAddress;
        }
    }
}