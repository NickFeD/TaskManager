using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TaskManager.Api.Models.Abstracted;
using TaskManager.Command.Models;

namespace TaskManager.Api.Entity
{
    [Table("participant")]
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

        [Column("user_id")]
        public User User { get; set; }

        [ForeignKey(nameof(User))]
        public int UserId { get; set; }

        [Column("project_id")]
        public Project Project { get; set; }

        [ForeignKey(nameof(Project))]
        public int ProjectId { get; set; }

        //public UserRole? Role { get; set; }

        //[Column("user_role_id")]
        //public int UserRoleId { get; set; }

        public static implicit operator ProjectParticipant(ProjectParticipantModel model)
        {
            return new ProjectParticipant()
            {
                ProjectId = model.ProjectId,
                UserId = model.UserId,
                Id = model.Id
            };
        }
        public static implicit operator ProjectParticipantModel(ProjectParticipant participant)
        {
            return new ProjectParticipant()
            {
                ProjectId = participant.ProjectId,
                UserId = participant.UserId,
                Id = participant.Id
            };
        }
    }
}
