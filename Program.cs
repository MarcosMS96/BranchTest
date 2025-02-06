using Microsoft.AspNetCore.Connections;
using Microsoft.EntityFrameworkCore;
using WebAPIProducto.Data;

var builder = WebApplication.CreateBuilder(args);

//Configuración para usar la base de datos SQLite

//Tomamos la cadena de conexión 

//Con builder.Configuration accedemos a la configuración de la aplicación
var connectionString = builder.Configuration
//Tomamos el valor de "DefaultConnection" definido en appsettings.json
.GetConnectionString("DefaultConnection");

//Nos permite registrar servicios en el contenedor de dependencias de la aplicación
builder.Services.
// Registra el contexto de la base de datos, el cual se utilizará para interactuar con la base de datos
AddDbContext<DataContext>(
    //Configuración para SQLite dandole la cadena de conexión
    options => options.UseSqlite(connectionString));

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
}; 

app.MapGet("/weatherforecast", () =>
{
    var forecast =  Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast")
.WithOpenApi();

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
