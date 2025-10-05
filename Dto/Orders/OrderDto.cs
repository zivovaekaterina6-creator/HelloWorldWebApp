namespace HelloWorld.Dto.Orders;

public class OrderDto
{
  public Guid Id { get; set; }

  public required string Name { get; set; }

  public decimal Cost { get; set; }
}