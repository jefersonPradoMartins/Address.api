using Address.Domain.Entities;
using Address.Service.Dto.Request.V1;
using Address.Service.External.Interface;
using Address.Service.Repositories.Interface;
using Asp.Versioning;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using Polly;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace Address.api.Controllers.v1
{
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class AddressController : Controller
    {

        private readonly IAddressService _addressS;
        private readonly IViaCepExternalService _viaCepS;
        private readonly ILogger<AddressController> _logger;
        private readonly IMapper _mapper;


        public AddressController(IAddressService addressS,
            IViaCepExternalService viaCepS,
            ILogger<AddressController> logger,
            IMapper mapper)
        {
            _addressS = addressS;
            _viaCepS = viaCepS;
            _logger = logger;
            _mapper = mapper;
        }

        //[HttpGet("teste")]
        //public IActionResult Get()
        //{
        //    int a = 5;  // Representação binária de 5: 0101
        //    int b = 3;  // Representação binária de 3: 0011

        //    a |= b;     // Agora 'a' terá o resultado da operação de OR bitwise: 0111 (7 em decimal)

        //    bool c = false;
        //    bool d = true;

        //    return Ok(c |= d);
        //}

        [ProducesResponseType<Domain.Entities.Address>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        public async Task<IActionResult> Create(CreateAddress address)
        {
            try
            {
                var newAddress = address.toAddress();
                _logger.LogInformation($"{address.ToString()}");
                await _addressS.CreateAsync(newAddress);
                Console.Out.WriteLine("Aqui passou");
                return Ok(newAddress);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpDelete("{addressId:Guid}")]
        public async Task<IActionResult> Delete(Guid addressId)
        {
            await _addressS.DeleteAsync(addressId);
            return Ok();
        }

        [OutputCache(Duration = 20)]
        [HttpGet("GetById/{addressId:Guid}")]
        public async Task<IActionResult> GetById(Guid addressId)
        {
            await _addressS.GetByIdAsync(addressId);
            return Ok();
        }


        [HttpGet("GetCep")]
        [OutputCache(Duration = 20)]
        public async Task<IActionResult> GetCep([FromQuery]
         [MaxLength(8, ErrorMessage = @"O campo Cep deve ser do tipo string 
            ou array com comprimento máximo de '8'.")]
        string cep)
        {

            var result = await _viaCepS.GetCepAsync(cep);
            _logger.LogInformation($"{DateTime.Now.ToString() + " " + result.ToString()}");
            return Ok(result);
        }

        [HttpGet("GetForecastAsync")]
        public async Task<IEnumerable<Domain.Entities.Address>> GetForecastAsync()
        {
            var retryPolicy = Policy<IEnumerable<Domain.Entities.Address>>
                .Handle<WebException>()
                .Or<Exception>()
                .RetryAsync(5, onRetry: (exception, retryCount, context) =>
                {
                    _logger.LogInformation($"JEF Retry #{retryCount} : Reason: {exception}");
                });

            var fallbackPolicy = Policy<IEnumerable<Domain.Entities.Address>>
                .Handle<WebException>()
                .Or<Exception>()
                .FallbackAsync((action) =>
                {
                    _logger.LogInformation("JEF All Retries Failed");
                    return null;
                });

            var reslt = await fallbackPolicy.WrapAsync(retryPolicy)
                .ExecuteAsync(() => _addressS.GetAllAsync());
            return reslt;
        }

    }
}
