using HelloWorld.Dto.Cities;
using HelloWorld.Entities;
using HelloWorld.Exceptions;
using HelloWorld.Services;
using Microsoft.AspNetCore.Mvc;

namespace HelloWorld.Controllers;


/// <summary>
/// Контроллер для работы с городами
/// </summary>
[ApiController]
[Route("cities")]
public class CitiesController : ControllerBase
{
  private readonly ICitiesUpdater _citiesUpdater;
  private readonly ICitiesProvider _citiesProvider;
  private readonly IDataBase _dataBase;
  private readonly IServiceProvider _serviceProvider;

  public CitiesController(
    IDataBase dataBase,
    ICitiesProvider citiesProvider,
    ICitiesUpdater citiesUpdater, 
    IServiceProvider serviceProvider)
  {
    _dataBase = dataBase;
    _citiesUpdater = citiesUpdater;
    _serviceProvider = serviceProvider;
    _citiesProvider = citiesProvider;
  }

  /// <summary>
  /// Получение всех существующих городов в системе
  /// </summary>
  /// <exception cref="NotFoundException"> Генерируется исключение если город не найден</exception>
  /// <returns>Список существующих городов</returns>
  [HttpGet]
  public CityDto[] GetCities()
  {
    Console.WriteLine(_dataBase.Id);
    return _serviceProvider.GetRequiredService<ICitiesProvider>().GetCities();
  }

  [HttpPost]
  public Guid CreateCity([FromBody] CityAddRequest city)
  {
    
    Console.WriteLine(_dataBase.Id);
    
    return _citiesUpdater.CreateCity(city);
  }

  [HttpPut("{id}")]
  public Guid CreateOrUpdateCity([FromRoute] Guid id, [FromBody] CityAddRequest city)
  {
    
    Console.WriteLine(_dataBase.Id);
    return _citiesUpdater.CreateOrUpdateCity(id, city);
  }

  [HttpGet("{id}")]
  public CityDto GetCity(Guid id)
  {
    
    Console.WriteLine(_dataBase.Id);
    return _citiesProvider.GetCity(id);
  }
  
  [HttpDelete("{id}")]
  public void DeleteCity(Guid id)
  {
    Console.WriteLine(_dataBase.Id);
    _citiesUpdater.DeleteCity(id);
  }
}