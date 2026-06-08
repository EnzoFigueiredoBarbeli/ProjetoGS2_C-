using Microsoft.EntityFrameworkCore;
using ProjetoGS.ApiService.Data;
using ProjetoGS.ApiService.Repositories;
using ProjetoGS.ApiService.Repositories.Interfaces;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

var connString = builder.Configuration.GetConnectionString("projetogs")
    ?? "Server=localhost;Port=3306;Database=projetogs;User=root;Password=root123;";

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(connString, ServerVersion.AutoDetect(connString)));

builder.Services.AddScoped<ITecnologiaRepository, TecnologiaRepository>();

// Ignora referências circulares no JSON
builder.Services.AddControllers().AddJsonOptions(options =>
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
    options.AddDefaultPolicy(p =>
        p.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
}

app.UseSwagger();
app.UseSwaggerUI();
app.UseCors();
app.MapControllers();
app.Run();
