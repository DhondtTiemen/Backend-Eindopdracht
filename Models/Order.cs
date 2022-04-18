namespace Eindopdracht.Models;

public class Order
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? OrderId { get; set; }
    public Customer Customer { get; set; }
    public Set Set { get; set; }
}