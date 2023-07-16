namespace TaskBackend.Models
{
    public class Step
    {
            public int Id { get; set; }
            public string? Name { get; set; }
            public string? Owner { get; set; }
            public List<Step>? SubSteps { get; set; }
    }
}

