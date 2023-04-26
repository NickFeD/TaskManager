using System;

namespace TaskManager.Api.Models
{
    public class Task:CommandObject
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public byte[] File { get; set; }
        public int DeskId { get; set; }
        public Desk Desk { get; set; }
        public string Column { get; set; }
        public int? СreatorId { get; set; }
        public User Сreator { get; set; }
        public int? ExecutorId { get; set; }
    }
}
