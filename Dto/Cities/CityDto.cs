using System.ComponentModel.DataAnnotations;

namespace HelloWorld.Dto.Cities;

public class CityDto
{
  public Guid Id { get; set; }

  public required string Name { get; set; }

  [MaxLength(5)]
  public string? Description { get; set; }
}