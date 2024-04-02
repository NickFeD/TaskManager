using TaskManager.Core.Entities;
using TaskManager.Core.Models.Project;

namespace TaskManager.Core.Extentions;

public static class ProjectExtension
{
    public static ProjectModel ToModel(this Project project)
    {
        return new()
        {
            Id = project.Id,
            Name = project.Name,
            Status = project.Status,
            CreatorId = project.CreatorId,
            Description = project.Description,
            CreationData = project.CreationData,
        };
    }

    public static Project ToEntity(this ProjectModel projectModel)
    {
        return new()
        {
            Id = projectModel.Id,
            Name = projectModel.Name,
            Status = projectModel.Status,
            CreatorId = projectModel.CreatorId,
            Description = projectModel.Description,
            CreationData = projectModel.CreationData,
        };
    }
}