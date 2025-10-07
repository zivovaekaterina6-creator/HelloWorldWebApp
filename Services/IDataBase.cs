using HelloWorld.Entities;

namespace HelloWorld.Services;

public interface IDataBase
{
    Guid Id { get; }
    Dictionary<Guid, CityEntity> Cities { get; }
    Dictionary<Guid, OrderEntity> Orders { get; }
    Dictionary<Guid, StudentEntity> Students { get; }
}