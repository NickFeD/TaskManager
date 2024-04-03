using Microsoft.AspNetCore.Mvc;

namespace TaskManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HealthzController : ControllerBase
    {
        [HttpGet()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult HealthCheck()
            => Ok();
    }
}
