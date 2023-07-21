using System.ComponentModel.DataAnnotations.Schema;
using TaskManager.Command.Models;

namespace TaskManager.Api.Entity
{
    public class Task
    {
#pragma warning disable CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Возможно, стоит объявить поле как допускающее значения NULL.
        public Task() { }
#pragma warning restore CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Возможно, стоит объявить поле как допускающее значения NULL.
        
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public DateTime CreationData { get; set; } = DateTime.Now;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int? СreatorId { get; set; }

        [ForeignKey(nameof(СreatorId))]
        public User Сreator { get; set; }

        public int DeskId { get; set; }

        [ForeignKey(nameof(DeskId))]
        public Desk Desk { get; set; }

        public TaskModel ToDto()
        {
            return new TaskModel
            {
                Id = Id,
                Name = Name,
                Description = Description,
                CreationData = CreationData,
                DeskId = DeskId,
                EndDate = EndDate,
                StartDate = StartDate,
                СreatorId = СreatorId,
            };
        }
    }
}
