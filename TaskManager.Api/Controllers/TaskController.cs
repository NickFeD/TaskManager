using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Api.Controllers.Abstracted;
using TaskManager.Api.Data;
using TaskManager.Api.Services;
using TaskManager.Command.Models;

namespace TaskManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : CRUDController<TaskModel, TaskService>
    {
        public TaskController(ApplicationContext context) : base(new(context))
        {
        }
    }
}
