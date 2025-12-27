using Microsoft.AspNetCore.Mvc;

namespace ComputerCompany.API.Controllers;

// Placeholder controller for future user and role administration
[ApiController]
[Route("api/users")]
public class UsersController : ControllerBase
{
    // Returns a simple confirmation that the user management endpoint is reachable
    [HttpGet]
    public IActionResult GetStatus()
    {
        return Ok("User management endpoint is available.");
    }
}
