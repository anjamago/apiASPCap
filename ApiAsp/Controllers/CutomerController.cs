using Microsoft.AspNetCore.Mvc;
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
            return Ok(data);
        }

        [HttpGet("get/customers/{id}")]
        public async Task<IActionResult> find(string id)
        {
            var data = await Services.Find(id);
            return Ok(data);
        }
    }
}
