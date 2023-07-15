//using TaskManager.Api.Data;
//using TaskManager.Api.Entity;
//using TaskManager.Api.Services.Abstracted;
//using TaskManager.Command.Models;

//namespace TaskManager.Api.Services
//{
//    public class DeskService : CRUDService<DeskModel,Desk>
//    {

//        public DeskService(ApplicationContext context) : base(context) { }

//        public override List<DeskModel> GetAll()
//        {
//            var temp = _context.Desks.Select(d => (DeskModel)d);
//            return _context.Desks.Select(d=>(DeskModel)d).ToList();
//        }
//    }
//}
