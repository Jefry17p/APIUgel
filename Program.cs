using APIproyectoUgel.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Agrega los servicios al contenedor
builder.Services.AddControllers().AddJsonOptions(options =>
{
    // Configura JSON para evitar errores de ciclos de referencia
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
});

builder.Services.AddDbContext<DbAae202Dbugelproyecto01Context>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("TuCadenaDeConexion")));

// Configura CORS si es necesario
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

// Agrega Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configuración del pipeline de HTTP
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("CorsPolicy"); // Aplica la política de CORS
app.UseAuthorization();

app.MapControllers(); // Mapea los controladores

app.Run();
