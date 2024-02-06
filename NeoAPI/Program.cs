using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using NeoAPI.Models;
using NeoAPI.ModelsViews;
using NeoAPI.ModelsDOCIng;
using NeoAPI.Models.NeoVieja;
using NeoAPI.Logic;
using NeoAPI.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers().AddJsonOptions(x =>x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
builder.Services.AddDbContext<DbNeoIiContext>(options =>options.UseSqlServer(builder.Configuration.GetConnectionString("Neo")), ServiceLifetime.Transient);
builder.Services.AddDbContext<ViewsContext>(options =>options.UseSqlServer(builder.Configuration.GetConnectionString("Neo")), ServiceLifetime.Transient);
builder.Services.AddDbContext<DOCIngContext>(options =>options.UseSqlServer(builder.Configuration.GetConnectionString("DOCIng")), ServiceLifetime.Transient);
builder.Services.AddDbContext<NeoViejaContext>(options =>options.UseSqlServer(builder.Configuration.GetConnectionString("NeoVieja")), ServiceLifetime.Transient);
builder.Services.AddAutoMapper(typeof(Program));//Configurar mapeos de Profiles
var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.useErrorHandlingMiddleware();
app.MapControllers();

app.Run();
