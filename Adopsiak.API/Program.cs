using Adopsiak.BILL.Services;
using Adopsiak.DAL;
using Adopsiak.DAL.Interfaces;
using Adopsiak.DAL.Repositories;
using Adopsiak.Infrastucture;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AdopsiakDbContext>(options =>
    {
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
            b => b.MigrationsAssembly("Adopsiak.API"));
    }
);
builder.Services.AddHttpClient<AnimalScraper>();  // Rejestracja HttpClient
builder.Services.AddScoped<IAnimalRepository, AnimalRepository>();  // Rejestracja repozytorium zwierząt
builder.Services.AddScoped<IAnimalScraper, AnimalScraper>();  // Rejestracja scraper'a
builder.Services.AddScoped<IAnimalService, AnimalService>();  // Rejestracja zwierza

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost",
        builder =>
        {
            builder.WithOrigins(
                    "http://localhost:4200")
                .AllowAnyMethod()
                .AllowAnyHeader();
        });
});

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors("AllowLocalhost");  // Użycie polityki CORS

app.MapControllers();

app.Run();