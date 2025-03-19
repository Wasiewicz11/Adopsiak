using Adopsiak.BILL.Services;
using Microsoft.AspNetCore.Mvc;

namespace Adopsiak.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class AnimalsController : ControllerBase
{
    private readonly IAnimalService _animalService;

    public AnimalsController(IAnimalService animalService)
    {
        _animalService = animalService;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var animals = await _animalService.GetAllAsync();
        return Ok(animals);
    }

    [HttpPost]
    public async Task<IActionResult> ScrapeAsync()
    {
        var numberOfAnimals = await _animalService.ScrapeAsync();
        return Ok(numberOfAnimals);
    }

    [HttpGet]
    public async Task<IActionResult> GetRandom()
    {
        var randomAnimals = await _animalService.GetRandomAsync();
        return Ok(randomAnimals);
    }
}