//using Microsoft.AspNetCore.Mvc;
//using TaskManager.Api.Data;
//using TaskManager.Command.Models.Abstracted;

//namespace TaskManager.Api.Services.Abstracted
//{
//    public abstract class CRUDService<TModel, TEntity> : ICRUDService<TModel> where TModel : Model where TEntity : TModel, new()
//    {
//        protected readonly ApplicationContext _context;
//        protected CRUDService(ApplicationContext context)
//        {
//            _context = context;
//        }

//        public abstract List<TModel> GetAll();

//        public virtual TModel? GetById(int id)
//        {
//            var entity = _context.Find<TEntity>(id);
//            _context.SaveChanges();
//            return entity is null ? null : (TModel)entity;
//        }

//        public virtual TModel? Create(TModel model)
//        {
//            TEntity entity = new();
//            _context.Add(entity);
//            _context.Entry(entity).CurrentValues.SetValues(model);
//            _context.SaveChanges();
//            return model;
//        }

//        public virtual void Update(TModel model)
//        {
//            var entity = _context.Find<TEntity>(model.Id);
//            if (entity is null)
//                return;
//            _context.Entry(entity).CurrentValues.SetValues(model);
//            _context.SaveChanges();
//        }

//        public virtual void Delete(int id)
//        {
//            var entity = _context.Find<TEntity>(id);
//            if (entity is null)
//                return;
//            _context.Remove<TEntity>(entity);
//            _context.SaveChanges();
//        }

//        public void DoAction(Action action)
//        {
//            try
//            {
//                action();
//            }
//            catch (Exception)
//            {

//                throw;
//            }
//        }
//        public TModel DoAction(Func<TModel> func)
//        {
//            try
//            {
//                return func();
//            }
//            catch (Exception)
//            {

//                throw;
//            }
//        }

//    }
//}
