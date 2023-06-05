using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.Api.Models;
using TaskManager.Api.Models.Data;
using TaskManager.Api.Models.Services;
using TaskManager.Command.Models;

namespace TaskManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProjectController : ControllerBase
    {
        private readonly ApplicationContext _db;
        private readonly ProjectsService _projectsServices;
        private readonly UsersService _usersServices;
        public ProjectController(ApplicationContext db)
        {
            _db = db;
            _projectsServices = new(_db);
            _usersServices = new(_db);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IEnumerable<ProjectModel>> Get() =>
            await _projectsServices.GetAll();

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var projectModel = _projectsServices.Get(id);
            return projectModel is null? NoContent() : Ok(projectModel);

        }
        [HttpPost]
        [Authorize(Roles = $"Admin,Editor")]
        public IActionResult Create([FromBody] ProjectModel projectModel)
        {
            if (projectModel == null)
            {
                return BadRequest();
            }
            var user = _usersServices.GetUser(HttpContext.User.Identity.Name);
              return _projectsServices.Create(projectModel)?Ok(): NotFound();
        }

        [HttpPatch("id")]
        [Authorize(Roles = $"Admin,Editor")]
        public IActionResult Update(int id, [FromBody] ProjectModel projectModel)
        {
            if (projectModel == null)
            {
                return BadRequest();
            }
            return _projectsServices.Update(id, projectModel) ? Ok() : NotFound();
        }

        [HttpDelete("id")]
        [Authorize(Roles = $"Admin,Editor")]
        public IActionResult Delete(int id)
        {
           return _projectsServices.Delete(id) ? Ok() : NotFound();
        }
    }
}
