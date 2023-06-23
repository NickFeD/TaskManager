using TaskManager.Api.Data;
using TaskManager.Api.Entity;
using TaskManager.Api.Services.Abstracted;
using TaskManager.Command.Models;

namespace TaskManager.Api.Services
{
    public class DeskService : ICRUDService<DeskModel>
    {
        private readonly ApplicationContext _context;

        public DeskService(ApplicationContext context)
        {
            _context = context;
        }

        public DeskModel Create(DeskModel model)
        {
            _context.Desks.Add(model);
            _context.SaveChanges();
            return model;
        }

        public void Delete(int id)
        {
            var deskToDelete = _context.Desks.Find(id);
            if (deskToDelete is null)
                return;
            _context.Desks.Remove(deskToDelete);
        }

        public List<DeskModel> GetAll()
        {
            var temp = _context.Desks.Select(d => (DeskModel)d);
            return _context.Desks.Select(d=>(DeskModel)d).ToList();
        }

        public DeskModel? GetById(int id)
        {
            var desk = _context.Desks.Find(id);
            return desk is null ? null : (DeskModel)desk;
        }

        public void Update(DeskModel model)
        {
            _context.Update(model);
            _context.SaveChanges();
        }
    }
}
