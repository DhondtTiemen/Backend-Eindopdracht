namespace Eindopdracht.Models;

public class Customer
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? CustomerId { get; set; }
    public string? Name { get; set; }
    public string? Email { get; set; }
}