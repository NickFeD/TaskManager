using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TaskManager.Api.Models.Abstracted;
using TaskManager.Command.Models;

namespace TaskManager.Api.Entity
{
    [Table("participants")]
    public class ProjectParticipant
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

        [Key]
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public User User { get; set; }

        [Column("user_id")]
        public int UserId { get; set; }

        public Project Project { get; set; }

        [Column("project_id")]
        public int ProjectId { get; set; }
        
        public UserRole? Role { get; set; }

        [Column("user_role_id")]
        public int? UserRoleId { get; set; }

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

        

        public static implicit operator ProjectParticipant(ProjectParticipantModel model)
        {
            return new ProjectParticipant()
            {
                ProjectId = model.ProjectId,
                UserId = model.UserId,
                UserRoleId = model.UserRoleId,
                Id = model.Id
            };
        }
        public static implicit operator ProjectParticipantModel(ProjectParticipant participant)
        {
            return new ProjectParticipant()
            {
                ProjectId = participant.ProjectId,
                UserId = participant.UserId,
                UserRoleId = participant.UserRoleId,
                Id = participant.Id,
            };
        }
    }
}
