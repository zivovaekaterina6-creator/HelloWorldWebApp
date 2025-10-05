using System.ComponentModel.DataAnnotations;

namespace HelloWorld.Dto.Cities;

public class CityAddRequest
{
  public required string Name { get; set; }

  public string? Description { get; set; }

 [Range(0, 100)]
  public int PeopleCount { get; set; }

}