using System.Text.RegularExpressions;
using Adopsiak.DAL.Interfaces;
using Adopsiak.DAL.Models;
using Adopsiak.DAL.Repositories;
using HtmlAgilityPack;
using Microsoft.IdentityModel.Tokens;

namespace Adopsiak.Infrastucture;

public class AnimalScraper : IAnimalScraper
{
    private readonly HttpClient _httpClient;
    private const String AdoptionPageUrl = "https://napaluchu.waw.pl/zwierzeta/zwierzeta-do-adopcji/";

    public AnimalScraper(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<Animal>> ScrapeAnimalsAsync()
    {
        var animals = new List<Animal>();

        var htmlDocument = new HtmlDocument();
        htmlDocument.LoadHtml(await _httpClient.GetStringAsync(AdoptionPageUrl));

        var animalNodes = htmlDocument.DocumentNode.SelectNodes("//div[contains(@class, 'pet-block')]");

        if (animalNodes == null)
        {
            return animals;
        }

        foreach (var node in animalNodes)
        {
            var animalDescription = node.SelectSingleNode(".//div[contains(@class, 'lower-content')]");
            
            var nameNode = animalDescription.SelectSingleNode(".//a");
            var name = nameNode.SelectSingleNode("text()").InnerText.Trim();
            if (name.Length < 3)
            {
                name = "Imie nieznane";
            }
            
            var breed = animalDescription.SelectSingleNode(".//li[1]").InnerText.Trim();
            
            var petTagsNode = animalDescription.SelectSingleNode(".//li[contains(@class, 'pet-tags')]");
            var petTags = petTagsNode.SelectSingleNode("text()").InnerText.Trim().Split(", ");
            var gender = petTags[0];
            var age = short.Parse(Regex.Match(petTags[1], @"\d+").Value);
            var weight = double.Parse(Regex.Match(petTags[2], @"\d+").Value);
            
            var locationNode = animalDescription.SelectSingleNode(".//li[contains(@class, 'pet-from-where')]");
            var location = locationNode?.InnerText.Trim() ?? "Nieznana lokalizacja";
            
            var imageNode = node.SelectSingleNode(".//img");
            var imageUrl = imageNode.GetAttributeValue("src", "");

            var petDetailsNode = node.SelectSingleNode(".//a");
            var petDetailsUrl = "https://napaluchu.waw.pl" + petDetailsNode.GetAttributeValue("href", "");
            
            
            
            animals.Add(new Animal
            {
                Name = name,
                Breed = breed,
                Gender = gender,
                Age = age,
                Weight = weight,
                Location = location,
                ImageUrl = imageUrl,
                PetDetailsUrl = petDetailsUrl
            });
        }

        return animals;


    }
}