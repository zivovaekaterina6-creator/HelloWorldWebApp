using HelloWorld.Entities;

namespace HelloWorld.Services;

public static class Database
{
  public static Dictionary<Guid, CityEntity> Cities = new();
  public static Dictionary<Guid, OrderEntity> Orders = new();
  public static Dictionary<Guid, StudentEntity> Students = new();
}