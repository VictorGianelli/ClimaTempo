using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClimaTempo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherController : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(typeof(Response), StatusCodes.Status200OK)]
        public IActionResult Get()
        {
            var response = new Response
            {
                Age = 7,
                Name = "Victor"
            };

            return Ok(response);
        }
    }
}
