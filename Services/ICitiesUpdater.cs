using HelloWorld.Dto.Cities;

namespace HelloWorld.Services;

public interface ICitiesUpdater
{
    Guid CreateCity(CityAddRequest city);

    Guid CreateOrUpdateCity(Guid id, CityAddRequest city);

    void DeleteCity(Guid id);
}