namespace Eindopdracht.Models;

public class Theme
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? ThemeId { get; set; }
    public string? Name { get; set; }
}