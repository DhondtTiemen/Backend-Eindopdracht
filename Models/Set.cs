namespace Eindopdracht.Models;

public class Set
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string SetId { get; set; }
    public int SetNumber { get; set; }
    public string Name { get; set; }
    public int? MinimalAge { get; set; }
    public int? Pieces { get; set; }
    public double? Price { get; set; }
    public int Stock { get; set; }
    public Theme Theme { get; set; }
}