namespace ComputerCompany.API.Models;

// Represents an inbound delivery used to increase warehouse stock levels
public class InboundDelivery
{
    public int Id { get; set; }

    // Timestamp when the delivery was registered
    public DateTime ReceivedAt { get; set; } = DateTime.UtcNow;

    // Article that was delivered to the warehouse
    public int ArticleId { get; set; }
    public Article Article { get; set; } = null!;

    // Number of units added to stock
    public int Quantity { get; set; }
}
