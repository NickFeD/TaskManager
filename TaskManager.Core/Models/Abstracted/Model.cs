using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManager.Core.Models.Abstracted
{
    public abstract class Model
    {
        [Column("id")]
        public Guid Id { get; set; }
    }
}
