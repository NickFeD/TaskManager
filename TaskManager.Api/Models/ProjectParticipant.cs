using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TaskManager.Api.Models.Abstracted;

namespace TaskManager.Api.Models
{
    [Table("participant")]
    public class ProjectParticipant : Model
    {
#pragma warning disable CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Возможно, стоит объявить поле как допускающее значения NULL.
        public ProjectParticipant()
        {
        }
#pragma warning restore CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Возможно, стоит объявить поле как допускающее значения NULL.

        public ProjectParticipant(User user, int userId, Project project, int projectId)
        {
            User = user;
            UserId = userId;
            Project = project;
            ProjectId = projectId;
        }

        public User User { get; set; }
        
        [Column("user_id")]
        public int UserId { get; set; }

        public Project Project { get; set; }
        
        [Column("project_id")]
        public int ProjectId { get; set; }

        //public UserRole? Role { get; set; }

        //[Column("user_role_id")]
        //public int UserRoleId { get; set; }
    }
}
