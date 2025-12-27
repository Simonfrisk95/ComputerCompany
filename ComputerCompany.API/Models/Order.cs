namespace ComputerCompany.API.Models;

// Represents a customer order placed through the internal system
public class Order
{
    public int Id { get; set; }

    // Timestamp when the order was created
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Current lifecycle status of the order (Created, Shipped, Completed)
    public string Status { get; set; } = "Created";

    // Collection of products included in the order
    public ICollection<OrderRow> OrderRows { get; set; } = new List<OrderRow>();
}
