using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManager.Api.Models.Abstracted
{
    public interface IModel
    {
        [Column("id")]
        public int Id { get; set; }
    }
}
