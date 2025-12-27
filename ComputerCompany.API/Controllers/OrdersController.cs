using ComputerCompany.API.Models;
using ComputerCompany.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace ComputerCompany.API.Controllers;

// Handles customer order requests and delegates business logic to the order service
[ApiController]
[Route("api/orders")]
public class OrdersController : ControllerBase
{
    private readonly OrderService _orderService;

    public OrdersController(OrderService orderService)
    {
        _orderService = orderService;
    }

    // Creates a new customer order and reserves stock from the warehouse
    [HttpPost]
    public async Task<ActionResult<Order>> CreateOrder(Dictionary<int, int> articleQuantities)
    {
        try
        {
            var order = await _orderService.CreateOrderAsync(articleQuantities);
            return Ok(order);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    // Retrieves all orders for warehouse and administrative overview
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
    {
        var orders = await _orderService.GetAllOrdersAsync();
        return Ok(orders);
    }

    // Updates the status of an order during its lifecycle
    [HttpPut("{orderId}/status")]
    public async Task<IActionResult> UpdateOrderStatus(int orderId, [FromBody] string status)
    {
        try
        {
            await _orderService.UpdateOrderStatusAsync(orderId, status);
            return NoContent();
        }
        catch (InvalidOperationException ex)
        {
            return NotFound(ex.Message);
        }
    }
}
