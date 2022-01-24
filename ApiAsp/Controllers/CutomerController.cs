using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace ApiAsp.Controllers
{
    [ApiController]
    [Route("PLColab.Customer/[controller]")]
    public class CutomerController : ControllerBase
    {
        private readonly  ApiAsp.services.CustomerServices Services;
        
        public CutomerController()
        {
            Services = new ApiAsp.services.CustomerServices();
        }

        [HttpGet("get/customers")]
        public IActionResult Index()
        {
            var data = Services.AllCutomer();
            if(data.Status == (int)HttpStatusCode.InternalServerError)
            {
                return BadRequest(data);
            }

            return Ok(data);
        }

        [HttpGet("get/customers/{id}")]
        public async Task<IActionResult> find(string id)
        {
            var data = await Services.Find(id);
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Create( ApiAsp.Models.Entitys.Customer customer )
        {
            var response =  await Services.CreateCustomer(customer);
            if(response.Status == (int)HttpStatusCode.InternalServerError)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> Update(ApiAsp.Models.Entitys.Customer customer)
        {
            var response = await Services.UpdateCustomer(customer);
            if (response.Status == (int)HttpStatusCode.InternalServerError)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        // Metodo que  me permite elimiar un registro
        [HttpDelete]
        public async Task<IActionResult> Delete(string id)
        {
            var response = await Services.DeleteCustomer(id);
            if (response.Status == (int)HttpStatusCode.InternalServerError)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}
