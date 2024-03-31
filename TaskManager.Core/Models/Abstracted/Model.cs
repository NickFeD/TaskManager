using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManager.Core.Models.Abstracted
{
    public abstract class Model<TId>
    {
        [Column("id")]
        public TId Id { get; set; }
    }
}
