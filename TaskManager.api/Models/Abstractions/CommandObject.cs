using System;
using TaskManager.Command.Models;

namespace TaskManager.Api.Models
{
    public class CommandObject
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreationData { get; set; }
        public byte[] Photo { get; set; }

        public CommandObject()
        {
            CreationData = DateTime.Now;
        }
        public CommandObject(CommandModel model)
        {
            Name = model.Name;
            Description = model.Description;
            Photo = model.Photo;
            CreationData = model.CreationData;
        }
    }
}
