using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Command.Models.Abstracted;

namespace TaskManager.Command.Models
{
    public class UserRoleModel: Model
    {
        public string Name { get; set; }

        public int ProjectId { get; set; }
    }
}
