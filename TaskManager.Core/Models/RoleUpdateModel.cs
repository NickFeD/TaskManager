using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Core.Models.Abstracted;

namespace TaskManager.Core.Models;

public class RoleUpdateModel:RoleAllowed
{
    public string Name { get; set; } = string.Empty;
}
