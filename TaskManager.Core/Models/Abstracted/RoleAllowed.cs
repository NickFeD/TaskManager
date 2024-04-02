using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Core.Models.Abstracted
{
    public abstract class RoleAllowed
    {
        public bool AllowedDeleteProject { get; set; } = false;
        public bool AllowedEditProject { get; set; } = false;
        public bool AllowedAddUsersProject { get; set; } = false;
    }
}
