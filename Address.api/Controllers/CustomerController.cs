using Address.Domain.Entities;
using Address.Service.Repositories.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Address.api.Controllers
{
    [Route("api/v1/[controller]")]
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
