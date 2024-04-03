namespace TaskManager.Core.Models
{
    public record Response<TModel>(bool IsSuccess = false, string? Reason = null, TModel? Model = null) where TModel : class;
    public record Response(bool IsSuccess = false, string? Reason = null);
}

