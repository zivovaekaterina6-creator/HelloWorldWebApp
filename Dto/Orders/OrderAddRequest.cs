namespace HelloWorld.Dto.Orders;

public class OrderAddRequest
{

  public required string Name { get; set; }

  public decimal Cost { get; set; }

}