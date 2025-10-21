using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HelloWorld.Attributes;

namespace HelloWorld.Data.Entities;

[Table("students")]
public class StudentEntity
{
    [Required]
    [Key]
    [Column("id")]
    public Guid Id { get; set; }
    
    [Required]
    [Column("name")]
    public required string Name { get; set; }
    
    [Required]
    [Column("surname")]
    public required string Surname { get; set; }
    
    [Required]
    [Range(16, 100)]
    [Column("age")]
    public required int Age { get; set; }
    
    [Required]
    [Column("group")]
    public required string Group { get; set; }
    
    [Required]
    [Column("email")]
    public required string Email { get; set; }

    [Column("about")]
    public required string? About { get; set; }
    
    [Column("city_id")]
    public required Guid CityId { get; set; }
    
    [ForeignKey("city_id")]
    public CityEntity City { get; set; }
}