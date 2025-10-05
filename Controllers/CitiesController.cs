using HelloWorld.Dto.Cities;
using HelloWorld.Entities;
using HelloWorld.Exceptions;
using HelloWorld.Services;
using Microsoft.AspNetCore.Mvc;

namespace HelloWorld.Controllers;

[ApiController]
[Route("cities")]
public class CitiesController : ControllerBase
{

  [HttpGet]
  public CityDto[] GetCities()
  {
    var cityDtos = Database.Cities
    .Select(city => new CityDto
    {
      Id = city.Key,
      Name = city.Value.Name,
      Description = city.Value.Description
    })
    .ToArray();

    return cityDtos;
  }

  [HttpPost]
  public Guid CreateCity([FromBody] CityAddRequest city)
  {
    var newCity = new CityEntity
    {
      Id = Guid.NewGuid(),
      Name = city.Name,
      Description = city.Description
    };

    Database.Cities.Add(newCity.Id, newCity);
    
    return newCity.Id;
  }

  [HttpPut("{id}")]
  public Guid CreateOrUpdateCity([FromRoute] Guid id, [FromBody] CityAddRequest city)
  {
    var newCity = new CityEntity
    {
      Id = id,
      Name = city.Name,
      Description = city.Description
    };

    Database.Cities[id] = newCity;

    return id;
  }

  [HttpGet("{id}")]
  public CityDto GetCity(Guid id)
  {

    if (Database.Cities.TryGetValue(id, out var cityEntity))
    {
      return new CityDto
      {
        Id = cityEntity.Id,
        Name = cityEntity.Name,
        Description = cityEntity.Description
      };
    }

    throw new NotFoundException(
      $"City with Id {id} was not found");
  }
  
  [HttpDelete("{id}")]
  public void DeleteCity(Guid id)
  {

    if (Database.Cities.TryGetValue(id, out var cityEntity))
    {
      Database.Cities.Remove(id);
    }

    throw new NotFoundException(
      $"City with Id {id} was not found");
  }
}