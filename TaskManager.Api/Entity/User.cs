using System.ComponentModel.DataAnnotations.Schema;
using TaskManager.Command.Models;

namespace TaskManager.Api.Entity
{
    public class User : UserModel
    {
        public List<ProjectParticipant>? Participants { get; set; } = new();
        public List<UserRefreshToken> RefreshTokens { get; set; } = new();
        
        public User()
        {
            RegistrationDate = DateTime.Now;
            LastLoginData = DateTime.Now;
        }
    }
}
