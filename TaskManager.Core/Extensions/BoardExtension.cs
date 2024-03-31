using TaskManager.Core.Entities;
using TaskManager.Core.Models;

namespace TaskManager.Core.Extentions;

public static class BoardExtension
{
    public static BoardModel ToModel(this Board board)
    {
        return new()
        {
            Id = board.Id,
            Description = board.Description,
            Name = board.Name,
            ProjectId = board.ProjectId,
            CreationData = board.CreationData,
        };
    }

    public static Board ToEntity(this BoardModel boardModel)
    {
        return new()
        {
            Id = boardModel.Id,
            Description = boardModel.Description,
            Name = boardModel.Name,
            ProjectId = boardModel.ProjectId,
            CreationData = boardModel.CreationData,
        };
    }
}