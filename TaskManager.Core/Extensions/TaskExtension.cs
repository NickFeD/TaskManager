using TaskManager.Core.Entities;
using TaskManager.Core.Models;

namespace TaskManager.Core.Extentions;

public static class TaskExtension
{
    public static TaskModel ToModel(this TaskEntity task)
    {
        return new()
        {
            Id = task.Id,
            Name = task.Name,
            BoardId = task.BoardId,
            EndDate = task.EndDate,
            СreatorId = task.СreatorId,
            StartDate = task.StartDate,
            Description = task.Description,
            CreationData = task.CreationData,
        };
    }

    public static TaskEntity ToEntity(this TaskModel taskModel)
    {
        return new()
        {
            Id = taskModel.Id,
            Name = taskModel.Name,
            BoardId = taskModel.BoardId,
            EndDate = taskModel.EndDate,
            СreatorId = taskModel.СreatorId,
            StartDate = taskModel.StartDate,
            Description = taskModel.Description,
            CreationData = taskModel.CreationData,
        };
    }
}