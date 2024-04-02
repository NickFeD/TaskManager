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
            AllowedAddUsersProject = role.AllowedAddUsersProject,
            AllowedDeleteProject = role.AllowedDeleteProject,
            AllowedEditProject = role.AllowedEditProject,
        };
    }

    public static Role ToEntity(this RoleModel roleModel)
    {
        return new()
        {
            Id = roleModel.Id,
            Name = roleModel.Name,
            ProjectId = roleModel.ProjectId,
            AllowedAddUsersProject = roleModel.AllowedAddUsersProject,
            AllowedDeleteProject = roleModel.AllowedDeleteProject,
            AllowedEditProject = roleModel.AllowedEditProject,
        };
    }
}