using ComputerCompany.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace ComputerCompany.API.Controllers;

// Registers inbound deliveries that increase warehouse stock levels
[ApiController]
[Route("api/inbound")]
public class InboundsController : ControllerBase
{
    private readonly InventoryService _inventoryService;

    public InboundsController(InventoryService inventoryService)
    {
        _inventoryService = inventoryService;
    }

    // Registers a delivery of computers or components into the warehouse
    [HttpPost]
    public async Task<IActionResult> RegisterInbound(
        int articleId,
        int quantity)
    {
        try
        {
            await _inventoryService.RegisterInboundDeliveryAsync(articleId, quantity);
            return Ok();
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
