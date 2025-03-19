using Adopsiak.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace Adopsiak.DAL;

public class AdopsiakDbContext : DbContext
{

    public AdopsiakDbContext(DbContextOptions<AdopsiakDbContext> options) : base(options)
    {
    }
    
    public DbSet<Animal> Animal { get; set; }
}