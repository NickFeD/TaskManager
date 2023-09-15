using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using TaskManager.Command.Models;

namespace TaskManager.Api.Entity
{
    [Table("projects")]
    public class Project:IEntity<ProjectModel>
    {
#pragma warning disable CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Возможно, стоит объявить поле как допускающее значения NULL.
        public Project()
#pragma warning restore CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Возможно, стоит объявить поле как допускающее значения NULL.
        {
            CreationData = DateTime.Now;
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public DateTime CreationData { get; set; } = DateTime.Now;
        public ProjectStatus Status { get; set; }
        public int? CreatorId { get; set; }
        [ForeignKey(nameof(CreatorId))]
        public User? Creator { get; set; }

        public List<ProjectParticipant> Participants { get; set; } = new();
        public List<Role>? Roles { get; set; } = new();
        public List<Desk>? Desks { get; set; } = new();

        public ProjectModel ToDto() => new()
        {
            CreationData = CreationData,
            CreatorId = CreatorId,
            Description = Description,
            Name = Name,
            Id = Id,
            Status = Status,
        };
    }
}
