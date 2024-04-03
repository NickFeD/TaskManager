namespace TaskManager.Core.Models.Abstracted
{
    public abstract class CommandModel : Model
    {

        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        public DateTime CreationData { get; set; } = DateTime.Now;
        // public byte[] Photo { get; set; }
    }
}
