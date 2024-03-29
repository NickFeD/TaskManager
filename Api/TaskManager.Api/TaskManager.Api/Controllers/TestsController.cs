using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Api.Data;
using TaskManager.Api.Services;

namespace TaskManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestsController : ControllerBase
    {
        private readonly Initialization initialization;
        private readonly HttpContextHandlerService _httpHandler;

        public TestsController(ApplicationContext context)
        {
            initialization = new Initialization(context);
            _httpHandler = new(context);
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
        [Authorize]
        [HttpGet("Authorize")]
        public IActionResult TestAuthorize()
        {
            return Ok("ok");
        }
    }
}
