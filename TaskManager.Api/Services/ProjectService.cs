using Microsoft.EntityFrameworkCore;
using TaskManager.Api.Data;
using TaskManager.Api.Entity;
using TaskManager.Api.Services.Abstracted;
using TaskManager.Command.Models;

namespace TaskManager.Api.Services
{
    public class ProjectService: CRUDService<ProjectModel,Project>
    {
        public ProjectService(ApplicationContext context) :base(context) { }

        public override List<ProjectModel> GetAll()
        {
            return _context.Projects.Select(p => (ProjectModel)p).ToList();
        }
    }
}
