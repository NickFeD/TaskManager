using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Command.Models
{
    public class UserRoleModel
    {
        public UserModel User { get; set; }
        public RoleModel Role { get; set; }
    }
}
