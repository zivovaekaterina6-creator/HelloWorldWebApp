using HelloWorld.Dto.Cities;
using HelloWorld.Entities;
using HelloWorld.Exceptions;
using HelloWorld.Services.Senders;

namespace HelloWorld.Services;

public class CitiesService : ICitiesUpdater, ICitiesProvider
{
  private readonly IDataBase _dataBase;
  private readonly IDictionary<string, Lazy<IMessageSender>> _senders;
  private readonly IServiceProvider _serviceProvider;

  public CitiesService(
    IDataBase dataBase,
    IEnumerable<IMessageSender> senders, 
    IServiceProvider serviceProvider)
  {
    _dataBase = dataBase;
    _serviceProvider = serviceProvider;
  }

  public CityDto[] GetCities()
      {
        Console.WriteLine(_dataBase.Id);
        
        var cityDtos = _dataBase.Cities
        .Select(city => new CityDto
        {
          Id = city.Key,
          Name = city.Value.Name,
          Description = city.Value.Description
        })
        .ToArray();
    
        return cityDtos;
      }
    
      public Guid CreateCity(CityAddRequest city)
      {
        
        Console.WriteLine(_dataBase.Id);
        
        var newCity = new CityEntity
        {
          Id = Guid.NewGuid(),
          Name = city.Name,
          Description = city.Description
        };
    
        _dataBase.Cities.Add(newCity.Id, newCity);
        
       //_serviceProvider.GetRequiredKeyedService<IMessageSender>("Sms").SendMessage($"Created city {newCity.Name}");
       _senders["SmsSender"].Value.SendMessage($"Created city {newCity.Name}"); 
       
        return newCity.Id;
      }
    
      public Guid CreateOrUpdateCity(Guid id, CityAddRequest city)
      {
        
        Console.WriteLine(_dataBase.Id);
        
        var newCity = new CityEntity
        {
          Id = id,
          Name = city.Name,
          Description = city.Description
        };
    
        _dataBase.Cities[id] = newCity;
    
        return id;
      }
    
      public CityDto GetCity(Guid id)
      {
    
        Console.WriteLine(_dataBase.Id);
        
        if (_dataBase.Cities.TryGetValue(id, out var cityEntity))
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
      
      public void DeleteCity(Guid id)
      {

        Console.WriteLine(_dataBase.Id);
        
        if (_dataBase.Cities.TryGetValue(id, out var cityEntity))
        {
          _dataBase.Cities.Remove(id);
        }
    
        throw new NotFoundException(
          $"City with Id {id} was not found");
      }
}