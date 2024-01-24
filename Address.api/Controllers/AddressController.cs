using Address.Service.Dto;
using Address.Service.External.Interface;
using Address.Service.Repositories.Interface;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Address.api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AddressController : Controller
    {

        private readonly IAddressService _addressS;
        private readonly IViaCepExternalService _viaCepS;

        public AddressController(IAddressService addressS, IViaCepExternalService viaCepS)
        {
            _addressS = addressS;
            _viaCepS = viaCepS;
        }

        [HttpPost]
        public async Task<IActionResult> Create(Domain.Entities.Address address)
        {
            try
            {
                await _addressS.CreateAsync(address);
                return Ok(address);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
        [HttpDelete("{addressId:Guid}")]
        public async Task<IActionResult> Delete(Guid addressId)
        {
            try
            {
                await _addressS.DeleteAsync(addressId);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpGet("GetById/{addressId:Guid}")]
        public async Task<IActionResult> GetById(Guid addressId)
        {
            try
            {
                await _addressS.GetByIdAsync(addressId);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
        //[HttpGet("{cep:alpha}")]
        // [HttpGet("{cep:int}")]
        [HttpGet("GetCep")]
        public async Task<IActionResult> GetCep([FromQuery]
         [MaxLength(8, ErrorMessage = "O campo Cep deve ser do tipo string ou array com comprimento máximo de '8'.")]
        string cep)
        {
          
                var result = await _viaCepS.GetCepAsync(cep);

                return Ok(result);
          

        }
    }
}
