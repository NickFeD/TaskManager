using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Core.Models.Abstracted;

namespace TaskManager.Core.Models
{
    public class ParticipantModel:Model
    {
        public Guid UserId { get; set; }
        public Guid ProjectId { get; set; }

        public Guid RoleId { get; set; }
    }
}
