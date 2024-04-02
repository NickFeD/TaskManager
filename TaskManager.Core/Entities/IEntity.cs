using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Core.Entities;

public interface IEntity<TId>
{
    [Required]
    public TId Id { get; set; }
}
