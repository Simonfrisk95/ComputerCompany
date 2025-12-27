using ComputerCompany.API.Data;
using ComputerCompany.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ComputerCompany.API.Controllers;

// Exposes warehouse inventory data for computers and hardware components
[ApiController]
[Route("api/articles")]
public class ArticlesController : ControllerBase
{
    private readonly AppDbContext _context;

    public ArticlesController(AppDbContext context)
    {
        _context = context;
    }

    // Retrieves all articles currently stored in the warehouse
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Article>>> GetAllArticles()
    {
        var articles = await _context.Articles.ToListAsync();
        return Ok(articles);
    }

    // Registers a new computer or hardware component in the warehouse
    [HttpPost]
    public async Task<ActionResult<Article>> CreateArticle(Article article)
    {
        _context.Articles.Add(article);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetAllArticles), new { id = article.Id }, article);
    }
}
