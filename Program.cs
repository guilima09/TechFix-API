using Microsoft.EntityFrameworkCore;
using TechFix_API.Data;



var builder = WebApplication.CreateBuilder(args);

// 1. Adicionar o suporte para Controllers e Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 2. Configurar a base de dados SQLite
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection") 
        ?? "Data Source=app.db"));

var app = builder.Build();

// 3. Configurar o Middleware e a Interface do Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();

// 4. Mapear as tuas rotas dos Controllers (Clientes e OrdensServico)
app.MapControllers();

app.Run();