using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiAsp.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class test1 : ControllerBase
    {
      
        [HttpGet("Edit")]
        public string Edit()
        {
            return  "hola";
        }

        [HttpGet("Detalle/{id}")]
        public string Detalle(int id)
        {
            return $"numero {id}";
        }

        [HttpPost("Detalle/{id}")]
        public string post(int id)
        {
            return $" post numero {id}";
        }
    }
}
