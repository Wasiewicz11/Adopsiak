using Adopsiak.DAL.Models;
using Adopsiak.DAL.Repositories;

namespace Adopsiak.BILL.Services;

public interface IAnimalService
{
    Task<int> ScrapeAsync();
    Task<List<Animal>> GetAllAsync();
    Task<List<Animal>> GetRandomAsync();

}

public class AnimalService : IAnimalService
{
    private readonly IAnimalRepository _animalRepository;

    public AnimalService(IAnimalRepository animalRepository)
    {
        _animalRepository = animalRepository;
    }
    
    public async Task<int> ScrapeAsync()
    {
        var numberOfAnimals = await _animalRepository.ScrapeAsync();

        return numberOfAnimals;
    }

    public async Task<List<Animal>> GetAllAsync()
    {
        var animals = await _animalRepository.GetAllAsync();

        return animals;
    }

    public async Task<List<Animal>> GetRandomAsync()
    {
        var randomAnimals = await _animalRepository.GetRandomAsync();

        return randomAnimals;
    }

    
}