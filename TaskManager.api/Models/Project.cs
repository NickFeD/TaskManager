using System;
using System.Collections.Generic;
using System.Linq;
using TaskManager.Command.Models;

namespace TaskManager.Api.Models
{
    public class Project : CommandObject
    {
        public int Id { get; set; }
        public int? AdminId { get; set; }
        public ProjectAdmin Admin { get; set; }
        public List<User> AllUsers { get; set; } = new();
        public List<Desk> AllDesks { get; set; } = new();
        public ProjectStatus Status { get; set; }

        public Project() { }
        public Project(ProjectModel projectModel) : base(projectModel)
        {
            Id = projectModel.Id;
            AdminId = projectModel.AdminId;
            Status = projectModel.Status;
        }

        internal ProjectModel ToDto() => new()
        {
            Id = Id,
            AdminId = AdminId,
            Description = Description,
            Name = Name,
            Photo = Photo,
            CreationData = CreationData,
            Status = Status,
        };
    }
}
