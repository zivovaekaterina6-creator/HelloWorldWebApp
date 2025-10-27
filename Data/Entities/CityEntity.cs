using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HelloWorld.Data.Entities;

[Table("cities")]
public class CityEntity
{
  [Key]
  [Column("id")]
  [Required]
  public required Guid Id { get; set; }

  [Column("name")]
  [Required]
  public required string Name { get; set; }

  [Column("description")]
  public string? Description { get; set; }

  [Column("people_count")]
  public long PeopleCount { get; set; }
  
  [Column("about")]
  public required string? About { get; set; }
  
  [Column("location")]
  public string Location { get; set; }
}