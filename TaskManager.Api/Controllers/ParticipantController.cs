using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Api.Data;
using TaskManager.Api.Models.Abstracted;
using TaskManager.Api.Services;
using TaskManager.Command.Models;

namespace TaskManager.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ParticipantController : CRUDController<ProjectParticipantModel, ParticipantService>
    {
        //[HttpGet("{id}")]
        //public ActionResult GetById(int id)
        //{
        //   return 
        //}
        public ParticipantController(ApplicationContext context) : base(new(context))
        {

        }
    }
}
