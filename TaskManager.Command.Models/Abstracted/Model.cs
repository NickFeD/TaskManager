using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManager.Command.Models.Abstracted
{
    public abstract class Model : IModel
    {
        [Column("id")]
        public int Id { get; set; }
    }
}
