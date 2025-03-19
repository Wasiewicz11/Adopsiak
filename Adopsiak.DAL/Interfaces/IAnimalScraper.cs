using Adopsiak.DAL.Models;

namespace Adopsiak.DAL.Interfaces;

public interface IAnimalScraper
{
    Task<List<Animal>> ScrapeAnimalsAsync();
}