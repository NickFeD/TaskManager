using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using TaskManager.Api.Controllers.Abstracted;
using TaskManager.Command.Models;

namespace TaskManager.Api.Entity
{
    [Table("projects")]
    public class Project: ProjectModel
    {
        public Project()
        {
            CreationData = DateTime.Now;
        }

        [ForeignKey(nameof(CreatorId))]
        public User? Creator { get; set; }

        public List<ProjectParticipant> Participants { get; set; } = new();
        public List<UserRole>? UserRoles { get; set; } = new();
        public List<Desk>? Desks { get; set; } = new();

    }
}
