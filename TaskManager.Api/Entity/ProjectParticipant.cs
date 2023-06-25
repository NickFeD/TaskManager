using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TaskManager.Api.Controllers.Abstracted;
using TaskManager.Command.Models;

namespace TaskManager.Api.Entity
{
    [Table("participants")]
    public class ProjectParticipant :ProjectParticipantModel
    {
#pragma warning disable CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Возможно, стоит объявить поле как допускающее значения NULL.
        public ProjectParticipant()
        {
        }
#pragma warning restore CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Возможно, стоит объявить поле как допускающее значения NULL.

        public ProjectParticipant(User user, Project project)
        {
            User = user;
            UserId = user.Id;
            Project = project;
            ProjectId = project.Id;
        }

        [ForeignKey(nameof(UserId))]
        public User User { get; set; }

        [ForeignKey(nameof(ProjectId))]
        public Project Project { get; set; }

        [ForeignKey(nameof(UserRoleId))]
        public UserRole? Role { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is ProjectParticipant participant &&
                   UserId == participant.UserId &&
                   ProjectId == participant.ProjectId;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(UserId, ProjectId);
        }
    }
}
