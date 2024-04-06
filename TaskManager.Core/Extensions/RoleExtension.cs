using TaskManager.Core.Entities;
using TaskManager.Core.Models;

namespace TaskManager.Core.Extentions;

public static class RoleExtension
{
    public static RoleModel ToModel(this Role role)
    {
        return new()
        {
            Id = role.Id,
            Name = role.Name,
            ProjectId = role.ProjectId,
            ProjectAddUsers = role.ProjectAddUsers,
            ProjectDelete = role.ProjectDelete,
            ProjectEdit = role.ProjectEdit,
        };
    }

    public static Role ToEntity(this RoleModel roleModel)
    {
        return new()
        {
            Id = roleModel.Id,
            Name = roleModel.Name,
            ProjectId = roleModel.ProjectId,
            ProjectAddUsers = roleModel.ProjectAddUsers,
            ProjectDelete = roleModel.ProjectDelete,
            ProjectEdit = roleModel.ProjectEdit,
        };
    }
}