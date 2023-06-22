using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;
using TaskManager.Command.Models;
using TaskManager.Command.Models.Abstracted;

namespace TaskManager.Api.Entity
{
    public class Desk :Model
    {
#pragma warning disable CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Возможно, стоит объявить поле как допускающее значения NULL.
        public Desk() { }
#pragma warning restore CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Возможно, стоит объявить поле как допускающее значения NULL.

        [Column("name")]
        public string Name { get; set; }

        [Column("description")]
        public string? Description { get; set; }

        [Column("creation_data")]
        public DateTime CreationData { get; set; } = DateTime.Now;

        [ForeignKey(nameof(Project))]
        public int ProjectId { get; set; }
        [Column("project_id")]
        public Project Project { get; set; }

        public static implicit operator Desk(DeskModel model)
        {
            return new()
            {
                Name = model.Name,
                Id = model.Id,
                CreationData = model.CreationData,
                Description =model.Description,
                ProjectId = model.ProjectId,
            };
        }

        public static implicit operator DeskModel(Desk desk)
        {
            return new()
            {
                Name = desk.Name,
                Id = desk.Id,
                CreationData = desk.CreationData,
                Description = desk.Description,
                ProjectId = desk.ProjectId,
            };
        }
    }
}
