using Address.Domain.Entities;
using Address.Service.Repositories.Interface;
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace Address.api.Controllers.v1
{
    [ApiVersion(1)]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class CustomerController : Controller
    {

        private readonly ICustomerService _customerS;

        public CustomerController(ICustomerService customerS)
        {
            _customerS = customerS;
        }

        [HttpPost]
        public async Task<IActionResult> Create(Customer customer)
        {
            try
            {
                await _customerS.CreateAsync(customer);

                return Ok();
            }
            catch (Exception ex)
            { return BadRequest(); }
        }

    }
}
