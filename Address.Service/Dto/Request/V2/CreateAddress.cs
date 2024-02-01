using System.ComponentModel.DataAnnotations;

namespace Address.Service.Dto.Request.V2
{
    public record class CreateAddress(
         [MaxLength(100, ErrorMessage = @"O campo Street deve ser do tipo 
        string ou array com comprimento máximo de '100'.")]
        string Street,
          [MaxLength(100, ErrorMessage = @"O campo City deve ser do tipo 
        string ou array com comprimento máximo de '100'.")]
        string City,
           [MaxLength(2, ErrorMessage = @"O campo State deve ser do tipo 
        string ou array com comprimento máximo de '2'.")]
        string State,
           [MaxLength(8, ErrorMessage = @"O campo ZipCode deve ser do tipo 
        string ou array com comprimento máximo de '8'.")]
        string ZipCode,
           [MaxLength(100, ErrorMessage = @"O campo Complements deve ser do tipo 
        string ou array com comprimento máximo de '100'.")]
        string Complements,
           [MaxLength(100, ErrorMessage = @"O campo Neighborhood deve ser do tipo 
        string ou array com comprimento máximo de '100'.")]
        string Neighborhood,
           [MaxLength(100, ErrorMessage = @"O campo Ibge deve ser do tipo 
        string ou array com comprimento máximo de '7'.")]
        string Ibge,
           [Required(ErrorMessage =@"O campo CustomerId é obrigatório")]
        Guid CustomerId)
    {
        public Domain.Entities.Address toAddress()
        {
            return new Domain.Entities.Address
            {
                Street = Street,
                City = City,
                State = State,
                ZipCode = ZipCode,
                Complements = Complements,
                Neighborhood = Neighborhood,
                Ibge = Ibge,
                CustomerId = CustomerId
            };
        }
    }
}
