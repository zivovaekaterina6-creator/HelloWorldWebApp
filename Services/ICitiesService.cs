using HelloWorld.Dto.Cities;

namespace HelloWorld.Services;

public interface ICitiesProvider
{
    CityDto[] GetCities();

    CityDto GetCity(Guid id);
}