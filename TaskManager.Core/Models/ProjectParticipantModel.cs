using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Core.Models.Abstracted;

namespace TaskManager.Core.Models
{
    public class ProjectParticipantModel:Model<Guid>
    {
        public int UserId { get; set; }
        public int ProjectId { get; set; }

        public int? UserRoleId { get; set; }
    }
}
