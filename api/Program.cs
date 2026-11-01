using BerberRandevu.Api.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Bağlantı dizesi depoda değil, user-secrets içinde duruyor
var baglantiDizesi = builder.Configuration.GetConnectionString("BerberDb")
    ?? throw new InvalidOperationException("BerberDb bağlantı dizesi bulunamadı. README'deki user-secrets adımına bakın.");

builder.Services.AddDbContext<BerberDbContext>(options =>
    options.UseSqlServer(baglantiDizesi));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.MapControllers();

app.Run();
