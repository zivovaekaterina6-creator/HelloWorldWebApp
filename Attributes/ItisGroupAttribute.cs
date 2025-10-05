using System.ComponentModel.DataAnnotations;
using HelloWorld.Exceptions;

namespace HelloWorld.Attributes;

public class ItisGroupAttribute : ValidationAttribute
{
    private HashSet<string> groups = new()
    {

        "11-406",
        "11-407",
        "11-408",
        "11-409"
    };
    
    public override bool IsValid(object? value)
    {
        var group = value as string;

        if (group is null)
            throw new BadRequestException($"Value must be string");

        return groups.Contains(group);
    }
}