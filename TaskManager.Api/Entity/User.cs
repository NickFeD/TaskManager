using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TaskManager.Api.Controllers.Abstracted;
using TaskManager.Command.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TaskManager.Api.Entity
{
    [Table("users")]
    public class User: UserModel
    {
        public List<ProjectParticipant>? Participants { get; set; } = new List<ProjectParticipant>();
        public User() 
        {
            RegistrationDate = DateTime.Now;
            LastLoginData = DateTime.Now;
        }
    }
}
