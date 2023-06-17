using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManager.Command.Models.Abstracted
{
    public interface IModel
    {
        [Column("id")]
        public int Id { get; set; }
    }
}
