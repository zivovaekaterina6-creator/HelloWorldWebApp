using System.ComponentModel.DataAnnotations;
using HelloWorld.Attributes;

namespace HelloWorld.Entities;

public class StudentEntity
{
    [Required]
    public Guid Id { get; set; }
    
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
    [RegularExpression(@"/^\S+@\S+\.\S+$/")]
    public required string Email { get; set; }
}