using Microsoft.EntityFrameworkCore;
using NeoAPI.Models;
using NeoAPI.ModelsViews;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DbNeoIiContext>(options =>options.UseSqlServer(builder.Configuration.GetConnectionString("Neo")), ServiceLifetime.Transient);
builder.Services.AddDbContext<ViewsContext>(options =>options.UseSqlServer(builder.Configuration.GetConnectionString("Neo")), ServiceLifetime.Transient);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
