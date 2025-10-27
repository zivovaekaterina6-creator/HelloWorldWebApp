using System.ComponentModel.DataAnnotations;

namespace HelloWorld.Settings;

public class VkIntegration
{
    [Required]
    public string ApiKey { get; set; } = null!;
    
    public TimeSpan Retry { get; set; } = TimeSpan.FromSeconds(1);
}