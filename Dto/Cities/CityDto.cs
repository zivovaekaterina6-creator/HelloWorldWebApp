using System.ComponentModel.DataAnnotations;

namespace HelloWorld.Dto.Cities;

/// <summary>
/// Моделька для передачи информации о городах в системе
/// </summary>
public class CityDto
{
  /// <summary>
  /// Уникальный идентификатор
  /// </summary>
  public Guid Id { get; set; }

  public required string Name { get; set; }

  [MaxLength(5)]
  public string? Description { get; set; }
}