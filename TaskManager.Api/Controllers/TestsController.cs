using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Api.Data;

namespace TaskManager.Api.Controllers;

//[Route("api/[controller]")]
//[ApiController]
//public class TestsController : ControllerBase
//{
//    private readonly Initialization initialization;

//    public TestsController( context)
//    {
//        initialization = new Initialization(context);
//    }

//    [HttpGet]
//    public IActionResult Test()
//    {
//        return Ok("OK");
//    }

//    [HttpGet("dbFull/{num}")]
//    public IActionResult DbFull(int num)
//    {
//        initialization.InitializationDb(num);
//        return Ok("DbFull");
//    }
//    [Authorize]
//    [HttpGet("Authorize")]
//    public IActionResult TestAuthorize()
//    {
//        return Ok("ok");
//    }
//}
