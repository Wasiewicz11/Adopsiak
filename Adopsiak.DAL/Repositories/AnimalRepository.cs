using Adopsiak.DAL.Interfaces;
using Adopsiak.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace Adopsiak.DAL.Repositories;


public interface IAnimalRepository
{
    Task<int> ScrapeAsync();
    Task<List<Animal>> GetAllAsync();
    Task<List<Animal>> GetRandomAsync();
    
}

public class AnimalRepository : IAnimalRepository
{
    private readonly AdopsiakDbContext _context;
    private readonly IAnimalScraper _animalScraper;

    public AnimalRepository(AdopsiakDbContext context, IAnimalScraper animalScraper)
    {
        _context = context;
        _animalScraper = animalScraper;
    }

    public async Task<int> ScrapeAsync()
    {
        var animals = await _animalScraper.ScrapeAnimalsAsync();

        foreach (var animal in animals)
        {
            _context.Animal.Add(animal);
        }
        
        await _context.SaveChangesAsync();

        return animals.Count;
    }

    public async Task<List<Animal>> GetAllAsync()
    {
        var animals = await _context.Animal.ToListAsync();

        return animals;
    }

    public async Task<List<Animal>> GetRandomAsync()
    {
        var randomAnimals = await _context.Animal
            .OrderBy(g => Guid.NewGuid())
            .Take(50)
            .ToListAsync();

        return randomAnimals;
    }
}