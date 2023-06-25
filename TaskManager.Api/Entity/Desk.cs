using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;
using TaskManager.Command.Models;
using TaskManager.Command.Models.Abstracted;

namespace TaskManager.Api.Entity
{
    public class Desk :DeskModel
    {
#pragma warning disable CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Возможно, стоит объявить поле как допускающее значения NULL.
        public Desk() { }
#pragma warning restore CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Возможно, стоит объявить поле как допускающее значения NULL.

        [ForeignKey(nameof(ProjectId))]
        public Project Project { get; set; }

        public List<Task> Tasks { get; set; }

    }
}
