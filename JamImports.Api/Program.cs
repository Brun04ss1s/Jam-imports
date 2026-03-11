using JamImports.Api.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// ==========================================================
// FASE 1: SERVIÇOS (Sempre ANTES do builder.Build())
// ==========================================================

builder.Services.AddOpenApi();

// 1. Avisa o .NET que vamos usar Controllers na arquitetura
builder.Services.AddControllers();

// 2. Configura a conexão com o PostgreSQL
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddSingleton<JamImports.Api.Services.KafkaProducerService>();

// Fecha a caixa de ferramentas e constrói o app
var app = builder.Build();

// ==========================================================
// FASE 2: PIPELINE HTTP (Sempre DEPOIS do builder.Build())
// ==========================================================

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

// 3. Ativa as rotas dos nossos Controllers (como o ProdutosController)
app.MapControllers();

// O exemplo padrão de clima que veio no template (pode deixar aí por enquanto)
var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast");

// Inicia o servidor
app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}