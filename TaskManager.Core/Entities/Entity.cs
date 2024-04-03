using System.ComponentModel.DataAnnotations;

namespace TaskManager.Core.Entities;

public class Entity<TId> : IEntity<TId>
{
    [Required]
    public TId Id { get; set; }
}
