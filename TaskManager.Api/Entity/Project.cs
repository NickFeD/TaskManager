using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using TaskManager.Api.Controllers.Abstracted;
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

        [Key]
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("description")]
        public string? Description { get; set; }

        [Column("creation_data")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime CreationData { get; set; } = DateTime.Now;

        [Column("creator_id")]
        public int? CreatorId { get; set; }

        [ForeignKey(nameof(CreatorId))]
        public User? Creator { get; set; }

        [Column("status")]
        public ProjectStatus Status { get; set; }
        public List<ProjectParticipant> Participants { get; set; } = new();
        public List<UserRole>? UserRoles { get; set; } = new();
        public List<Desk> Desks { get; set; } = new();


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
