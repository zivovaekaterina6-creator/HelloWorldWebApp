using HelloWorld.Data;
using HelloWorld.Data.Entities;
using HelloWorld.Dto.Cities;
using HelloWorld.Entities;
using HelloWorld.Exceptions;
using HelloWorld.Services.Senders;
using Microsoft.EntityFrameworkCore;

namespace HelloWorld.Services;

public class CitiesService : ICitiesUpdater, ICitiesProvider
{
  private readonly IDataBase _dataBase;
  private readonly IDictionary<string, Lazy<IMessageSender>> _senders;
  private readonly IServiceProvider _serviceProvider;
  private readonly ApplicationDbContext _applicationDbContext;

  public CitiesService(
    IDataBase dataBase,
    IEnumerable<IMessageSender> senders, 
    IServiceProvider serviceProvider,
    ApplicationDbContext applicationDbContext)
  {
    _dataBase = dataBase;
    _serviceProvider = serviceProvider;
    _applicationDbContext = applicationDbContext;
  }

  public CityDto[] GetCities()
      {
        Console.WriteLine(_dataBase.Id);
        
        var cityDtos = _applicationDbContext.Cities
        .Select(city => new CityDto
        {
          Id = city.Id,
          Name = city.Name,
          Description = city.Description
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
          Description = city.Description,
          About = ""
        };
    
        _applicationDbContext.Cities.Add(newCity);
        _applicationDbContext.SaveChanges();
        
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
          Description = city.Description,
          About = ""
        };
    
        _applicationDbContext.Cities.Attach(newCity);
        _applicationDbContext.SaveChanges();
    
        return id;
      }
    
      public CityDto GetCity(Guid id)
      {
    
        Console.WriteLine(_dataBase.Id);

        var cityEntity = _applicationDbContext.Cities.Find(id);
        if (cityEntity != null)
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
        
        var cityEntity = _applicationDbContext.Cities.Find(id);
        
        if (cityEntity != null)
        {
          _applicationDbContext.Cities.Remove(cityEntity);
        }
    
        throw new NotFoundException(
          $"City with Id {id} was not found");
      }
}