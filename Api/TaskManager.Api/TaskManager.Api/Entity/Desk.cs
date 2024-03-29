using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;
using TaskManager.Command.Models;
using TaskManager.Command.Models.Abstracted;

namespace TaskManager.Api.Entity
{
    public class Desk
    {
#pragma warning disable CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Возможно, стоит объявить поле как допускающее значения NULL.
        public Desk() { }
#pragma warning restore CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Возможно, стоит объявить поле как допускающее значения NULL.
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public DateTime CreationData { get; set; } = DateTime.Now;
        public int ProjectId { get; set; }
        [ForeignKey(nameof(ProjectId))]
        public Project Project { get; set; }

        public List<Task> Tasks { get; set; }


        public DeskModel ToDto() 
        {
            return new DeskModel
            {
                Id = Id,
                CreationData = CreationData,
                Description = Description,
                Name = Name,
                ProjectId = ProjectId,
            };  
        }
    }
}
