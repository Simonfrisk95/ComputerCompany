using ComputerCompany.API.Data;
using ComputerCompany.API.Models;
using Microsoft.EntityFrameworkCore;

namespace ComputerCompany.API.Services;

// Manages the full lifecycle of customer orders and their impact on warehouse stock
public class OrderService
{
    private readonly AppDbContext _context;
    private readonly InventoryService _inventoryService;

    public OrderService(AppDbContext context, InventoryService inventoryService)
    {
        _context = context;
        _inventoryService = inventoryService;
    }

    // Creates a new customer order and reserves the required stock from the warehouse
    public async Task<Order> CreateOrderAsync(Dictionary<int, int> articleQuantities)
    {
        var order = new Order();

        foreach (var entry in articleQuantities)
        {
            var articleId = entry.Key;
            var quantity = entry.Value;

            var article = await _context.Articles
                .FirstOrDefaultAsync(a => a.Id == articleId);

            if (article == null)
                throw new InvalidOperationException("One or more ordered articles do not exist.");

            if (article.StockQuantity < quantity)
                throw new InvalidOperationException("Not enough stock to fulfill the order.");

            order.OrderRows.Add(new OrderRow
            {
                ArticleId = articleId,
                Quantity = quantity
            });

            await _inventoryService.ReduceStockAsync(articleId, quantity);
        }

        _context.Orders.Add(order);
        await _context.SaveChangesAsync();

        return order;
    }

    // Updates the status of an existing order (e.g. Shipped, Completed)
    public async Task UpdateOrderStatusAsync(int orderId, string status)
    {
        var order = await _context.Orders
            .FirstOrDefaultAsync(o => o.Id == orderId);

        if (order == null)
            throw new InvalidOperationException("Order not found.");

        order.Status = status;
        await _context.SaveChangesAsync();
    }

    // Retrieves all orders for administrative or warehouse overview
    public async Task<List<Order>> GetAllOrdersAsync()
    {
        return await _context.Orders
            .Include(o => o.OrderRows)
            .ThenInclude(r => r.Article)
            .ToListAsync();
    }
}
