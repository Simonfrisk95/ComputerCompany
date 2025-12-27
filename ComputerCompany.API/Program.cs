using ComputerCompany.API.Data;
using ComputerCompany.API.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// === Controllers & Swagger ===
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// === Database ===
// SQLite används för ComputerCompanys lager- och ordersystem
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=computercompany.db"));

// === Application Services ===
builder.Services.AddScoped<InventoryService>();
builder.Services.AddScoped<OrderService>();

var app = builder.Build();

// === HTTP pipeline ===
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

// 🔴 KRITISK RAD – aktiverar Controllers
app.MapControllers();

app.Run();
