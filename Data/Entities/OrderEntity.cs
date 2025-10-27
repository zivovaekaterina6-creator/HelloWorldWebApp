namespace HelloWorld.Entities;

public class OrderEntity
{
  public Guid Id { get; set; }

  public string Name { get; set; }

  public decimal Cost { get; set; }

  public string Description { get; set; }
}