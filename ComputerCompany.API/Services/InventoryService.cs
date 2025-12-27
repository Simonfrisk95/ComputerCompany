using ComputerCompany.API.Data;
using ComputerCompany.API.Models;
using Microsoft.EntityFrameworkCore;

namespace ComputerCompany.API.Services;

// Handles all warehouse stock adjustments for computers and hardware components
public class InventoryService
{
    private readonly AppDbContext _context;

    public InventoryService(AppDbContext context)
    {
        _context = context;
    }

    // Increases stock levels when new computers or components are delivered to the warehouse
    public async Task RegisterInboundDeliveryAsync(int articleId, int quantity)
    {
        var article = await _context.Articles
            .FirstOrDefaultAsync(a => a.Id == articleId);

        if (article == null)
            throw new InvalidOperationException("Article not found in warehouse inventory.");

        article.StockQuantity += quantity;

        var inbound = new InboundDelivery
        {
            ArticleId = articleId,
            Quantity = quantity
        };

        _context.InboundDeliveries.Add(inbound);
        await _context.SaveChangesAsync();
    }

    // Reduces stock levels when computers or components are reserved for customer orders
    public async Task ReduceStockAsync(int articleId, int quantity)
    {
        var article = await _context.Articles
            .FirstOrDefaultAsync(a => a.Id == articleId);

        if (article == null)
            throw new InvalidOperationException("Article not found in warehouse inventory.");

        if (article.StockQuantity < quantity)
            throw new InvalidOperationException("Insufficient stock available.");

        article.StockQuantity -= quantity;
        await _context.SaveChangesAsync();
    }
}
