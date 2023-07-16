namespace TaskBackend.Models
{
    public class Task
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public List<Step>? Steps { get; set; }
    }
}

