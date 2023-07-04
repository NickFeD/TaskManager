using System.ComponentModel.DataAnnotations.Schema;
using TaskManager.Command.Models;

namespace TaskManager.Api.Entity
{
    public class Task: TaskModel
    {
#pragma warning disable CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Возможно, стоит объявить поле как допускающее значения NULL.
        public Task() { }
#pragma warning restore CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Возможно, стоит объявить поле как допускающее значения NULL.


        [ForeignKey(nameof(СreatorId))]
        public User Сreator { get; set; }

        [ForeignKey(nameof(DeskId))]
        public Desk Desk { get; set; }
    }
}
