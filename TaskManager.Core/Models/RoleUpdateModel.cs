﻿using TaskManager.Core.Models.Abstracted;

namespace TaskManager.Core.Models;

public class RoleUpdateModel : RoleAllowed
{
    public string Name { get; set; } = string.Empty;
}
