using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Api.Data;

namespace TaskManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestsController : ControllerBase
    {
        Initialization initialization;

        public TestsController(ApplicationContext context)
        {
            initialization = new Initialization(context);
        }

        [HttpGet]
        public IActionResult Test()
        {
            return Ok("OK");
        }

        [HttpGet("dbFull/{num}")]
        public IActionResult DbFull(int num)
        {
            initialization.InitializationDb(num);
            return Ok("DbFull");
        }
    }
}
