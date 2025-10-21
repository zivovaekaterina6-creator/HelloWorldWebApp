using HelloWorld.Data.Entities;
using HelloWorld.Entities;

namespace HelloWorld.Services;

public class Database : IDataBase
{
    public Guid Id { get; } = Guid.NewGuid();
  public Dictionary<Guid, CityEntity> Cities { get; } = new();
  public Dictionary<Guid, OrderEntity> Orders { get; } = new();
  public Dictionary<Guid, StudentEntity> Students { get; } = new();

  public Database()
  {
     Console.WriteLine("Created new instance");
  }
}