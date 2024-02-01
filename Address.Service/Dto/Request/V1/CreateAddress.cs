
using System.ComponentModel.DataAnnotations;

namespace Address.Service.Dto.Request.V1
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
        [MaxLength(100, ErrorMessage = @"O campo Neighborhood deve ser do tipo 
        string ou array com comprimento máximo de '100'.")]
        string Neighborhood,
        [Required(ErrorMessage =@"O campo CustomerId é obrigatório")]
        Guid CustomerId)
    { 
    
        public Domain.Entities.Address toAddress()
        {
            return new Domain.Entities.Address
            {
                Street = this.Street,
                City = this.City,
                State = this.State,
                ZipCode = this.ZipCode,
                Neighborhood = this.Neighborhood,
                CustomerId = CustomerId
            };
        }
    };
}
