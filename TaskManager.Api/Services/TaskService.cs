using TaskManager.Api.Data;
using TaskManager.Api.Services.Abstracted;
using TaskManager.Command.Models;

namespace TaskManager.Api.Services
{
    public class TaskService : CRUDService<TaskModel, Entity.Task>
    {
        public TaskService(ApplicationContext context) : base(context)
        {
        }

        public override TaskModel? Create(TaskModel model)
        {
            var user = _context.Users.Find(model.СreatorId);
            if (user is null)
                return null;
            return base.Create(model);
        }

        public override List<TaskModel> GetAll()
        {
            return _context.Tasks.Select(t=>(TaskModel)t).ToList();
        }
    }
}
