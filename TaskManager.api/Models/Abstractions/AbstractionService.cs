using System;

namespace TaskManager.Api.Models.Abstractions
{
    public abstract class AbstractionService
    {
        protected static bool DoAction(Action action)
        {
            try
            {
                action.Invoke();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
