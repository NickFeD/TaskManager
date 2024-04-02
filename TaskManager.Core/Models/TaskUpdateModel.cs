using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Core.Models
{
    public class TaskUpdateModel
    {
        public required string Name { get; set; }
        public string? Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int BoardId { get; set; }
    }
}
