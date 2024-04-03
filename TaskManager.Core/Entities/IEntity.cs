using System.ComponentModel.DataAnnotations;

namespace TaskManager.Core.Entities;

public interface IEntity<TId>
{
    [Required]
    public TId Id { get; set; }
}
