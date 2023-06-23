using Microsoft.AspNetCore.Mvc;
using TaskManager.Api.Data;
using TaskManager.Command.Models.Abstracted;

namespace TaskManager.Api.Services.Abstracted
{
    public abstract class CRUTService<TModel, TEntity> : ICRUDService<TModel> where TModel : Model where TEntity : TModel
    {
        protected ApplicationContext _context;
        protected CRUTService(ApplicationContext context)
        {
            this._context = context;
        }

        public abstract List<TModel> GetAll();

        public TModel? GetById(int id)
        {
            var entity = _context.Find<TEntity>(id);
            _context.SaveChanges();
            return entity is null ? null : (TModel)entity;
        }

        public TModel Create(TModel model)
        {
            _context.Add((TEntity)model);
            _context.SaveChanges();
            return model;
        }

        public void Update(TModel model)
        {
            var entity = _context.Find<TEntity>(model.Id);
            if (entity is null)
                return;
            _context.Entry(entity).CurrentValues.SetValues(model);
        }

        public void Delete(int id)
        {
            var entity = _context.Find<TEntity>(id);
            if (entity is null)
                return;
            _context.Remove<TEntity>(entity);
        }

        public void DoAction(Action action)
        {
            try
            {
                action();
            }
            catch (Exception)
            {

                throw;
            }
        }
        public TModel DoAction(Func<TModel> func)
        {
            try
            {
                return func();
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
