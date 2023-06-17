using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using TaskManager.Api.Models.Abstracted;
using TaskManager.Command.Models;

namespace TaskManager.Api.Models
{
    [Table("projects")]
    public class Project : Abstracted.Model
    {
        public Project()
        {
            CreationData = DateTime.Now;
        }

        [Column("name")]
        public string Name { get; set; }

        [Column("description")]
        public string? Description { get; set; }

        [Column("creation_data")]
        public DateTime CreationData { get; set; } = DateTime.Now;

        [Column("creator_id")]
        public int? CreatorId { get; set; }

        public User? Creator { get; set; }

        [Column("status")]
        public ProjectStatus Status { get; set; }
        public List<ProjectParticipant> Participants { get; set; } = new();
        public List<UserRole>? UserRoles { get; set; } = new();

        public static implicit operator Project(ProjectModel model)
        {
            return new Project()
            {
                Id = model.Id,
                Name = model.Name,
                Description = model.Description,
                CreatorId = model.CreatorId,
                Status = model.Status,
                CreationData = model.CreationData,
            };
        }
        public static implicit operator ProjectModel(Project model)
        {
            return new ProjectModel() 
            {
                Id = model.Id,
                Name = model.Name,
                Description = model.Description,
                CreatorId = model.CreatorId,
                Status = model.Status,
                CreationData = model.CreationData,
            };

        }
    }
}
