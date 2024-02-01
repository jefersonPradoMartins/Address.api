using Address.Service.Dto.Request.V2;
using Address.Service.External.Interface;
using Address.Service.Repositories.Interface;
using Asp.Versioning;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;

namespace Address.api.Controllers.v2
{
    [ApiVersion(2)]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class AddressController : Controller
    {
        private readonly IAddressService _addressS;
        private readonly IViaCepExternalService _viaCepS;
        private readonly ILogger<v1.AddressController> _logger;
        private readonly IMapper _mapper;

        public AddressController(IAddressService addressS,
          IViaCepExternalService viaCepS,
          ILogger<v1.AddressController> logger,
          IMapper mapper)
        {
            _addressS = addressS;
            _viaCepS = viaCepS;
            _logger = logger;
            _mapper = mapper;
        }


        [MapToApiVersion(2)]
        [OutputCache(Duration = 20)]
        [HttpGet("GetById/{addressId:Guid}")]
        public async Task<IActionResult> GetById(Guid addressId)
        {
            await _addressS.GetByIdAsync(addressId);
            return Ok();
        }

        /// <summary>
        /// Added two more properties.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Create(CreateAddress address)
        {
            var newAddress = address.toAddress();

            await _addressS.CreateAsync(newAddress);
            _logger.LogInformation($"{address.ToString()}");
            return Ok(address);
        }
    }
}
