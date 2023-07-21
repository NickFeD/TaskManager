using System.ComponentModel.DataAnnotations;
using TaskManager.Command.Models.Abstracted;

namespace TaskManager.Command.Models
{
    public record Response<TModel>(bool IsSuccess = false, string Reason = null, TModel Model = null) where TModel : class;
    public record Response(bool IsSuccess = false, string? Reason = null );
}

