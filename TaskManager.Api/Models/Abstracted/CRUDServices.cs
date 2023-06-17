using TaskManager.Api.Data;

namespace TaskManager.Api.Models.Abstracted
{
    public abstract class CRUTService<T> : ICRUDService<T>
    {
        protected ApplicationContext _context;
        protected CRUTService(ApplicationContext context) 
        {
            this._context = context;
        }
        public abstract T Get(int id);
        public abstract T Create(T model);

        public abstract void Delete(int id);

        public abstract void Update(T model);

        internal T GetModel(int id)
            => DoAcrion(() => Get(id));

        internal T CreateModel(T model) 
            => DoAcrion(() => Create(model));

        internal void DeleteModel(int id) 
            => DoAcrion(() => Delete(id));

        internal void UpdateModel(T model) 
            => DoAcrion(() => Update(model));

        public void DoAcrion(Action action)
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
        public T DoAcrion(Func<T> func)
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
