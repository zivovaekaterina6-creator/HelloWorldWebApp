using System.ComponentModel.DataAnnotations;
using HelloWorld.Attributes;

namespace HelloWorld.Dto.Students;

public class StudentAddRequest
{
    [Required]
    [MaxLength(100)]
    public required string Name { get; set; }
    
    [Required]
    [MaxLength(100)]
    public required string Surname { get; set; }
    
    [Required]
    [Range(16, 100)]
    public required int Age { get; set; }
    
    [Required]
    [ItisGroup]
    public required string Group { get; set; }
    
    [Required]
    [RegularExpression(@"^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}$")]
    public required string Email { get; set; }
    
    [Required]
    public required Guid CityId { get; set; }
}