namespace Eindopdracht.Models;

public class Set
{
    public string? SetId { get; set; }
    public string? Name { get; set; }
    public int MinimalAge { get; set; }
    public int Pieces { get; set; }
    public double Price { get; set; }
    public Theme? Theme { get; set; }
}