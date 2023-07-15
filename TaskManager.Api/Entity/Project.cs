using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using TaskManager.Command.Models;

namespace TaskManager.Api.Entity
{
    [Table("projects")]
    public class Project
    {
        public Project()
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
        public List<UserRole>? UserRoles { get; set; } = new();
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
