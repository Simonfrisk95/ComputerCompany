namespace ComputerCompany.API.Models;

// Represents a computer or hardware component stored in the company warehouse
public class Article
{
    public int Id { get; set; }

    // Human-readable name of the computer or component
    public string Name { get; set; } = string.Empty;

    // Stock Keeping Unit used to uniquely identify the product internally
    public string SKU { get; set; } = string.Empty;

    // Current quantity available in the warehouse
    public int StockQuantity { get; set; }

    // Physical warehouse location (e.g. A3-SHELF-2)
    public string Location { get; set; } = string.Empty;

    // Navigation property used to track which order rows reference this article
    public ICollection<OrderRow> OrderRows { get; set; } = new List<OrderRow>();
}
