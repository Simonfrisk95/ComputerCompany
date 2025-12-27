namespace ComputerCompany.API.Models;

// Represents a single line in an order, linking a product to a quantity
public class OrderRow
{
    public int Id { get; set; }

    // Foreign key to the order this row belongs to
    public int OrderId { get; set; }
    public Order Order { get; set; } = null!;

    // Foreign key to the ordered article
    public int ArticleId { get; set; }
    public Article Article { get; set; } = null!;

    // Number of units ordered for this specific article
    public int Quantity { get; set; }
}
