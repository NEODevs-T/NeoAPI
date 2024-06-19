using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using NeoAPI.Models.Neo;
using NeoAPI.ModelsDOCIng;
using NeoAPI.Models.PolybaseBPCSVen;
using NeoAPI.Models.PolybaseBPCSCol;
using NeoAPI.Models.PolybaseBPCSCen;
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
builder.Services.AddDbContext<DOCIngContext>(options =>options.UseSqlServer(builder.Configuration.GetConnectionString("DOCIng")), ServiceLifetime.Transient);
builder.Services.AddDbContext<PolybaseBPCSVenContext>(options =>options.UseSqlServer(builder.Configuration.GetConnectionString("PolybaseVen")), ServiceLifetime.Transient);
builder.Services.AddDbContext<PolybaseBPCSColContext>(options =>options.UseSqlServer(builder.Configuration.GetConnectionString("PolybaseCol")), ServiceLifetime.Transient);
builder.Services.AddDbContext<PolybaseBPCSCenContext>(options =>options.UseSqlServer(builder.Configuration.GetConnectionString("PolybaseCen")), ServiceLifetime.Transient);

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
