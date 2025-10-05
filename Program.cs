using HelloWorld.Dto.Orders;
using HelloWorld.Entities;
using HelloWorld.Exceptions;
using HelloWorld.Filters;
using HelloWorld.Middlewares;
using HelloWorld.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();

builder.Services.AddMvc(
  options => 
    options.Filters.Add<HttpExceptionFilter>());

var app = builder.Build();

//app.UseMiddleware<HttpExceptionHandlerMiddleware>();
app.MapControllers();

app.MapGet("/", () => "Hello World!")
.AddEndpointFilter<HttpExceptionEndPointFilter>();

app.MapGet("/orders", () => Database.Orders.Values)
.AddEndpointFilter<HttpExceptionEndPointFilter>();

app.MapGet("/orders/{id}", (Guid id) =>
  {
    if (!Database.Orders.ContainsKey(id))
      throw new NotFoundException($"Order with id: {id} was not found");
    
    var order = Database.Orders[id];

    return new OrderDto
    {
      Id = order.Id,
      Name = order.Name,
      Cost = order.Cost
    };
  })
.AddEndpointFilter<HttpExceptionEndPointFilter>();


app.MapDelete("/orders/{id}", (Guid id) =>
  {
    if (!Database.Orders.ContainsKey(id))
      throw new NotFoundException($"Order with id: {id} was not found");

    Database.Orders.Remove(id);
  })
  .AddEndpointFilter<HttpExceptionEndPointFilter>();

app.MapPost("/orders", (OrderAddRequest addRequest) =>
  {
    var newId = Guid.NewGuid();
    
    Database.Orders.Add(newId, new OrderEntity
    {
      Id = newId,
      Name = addRequest.Name,
      Cost = addRequest.Cost
    });  
  })
  .AddEndpointFilter<HttpExceptionEndPointFilter>();

app.MapPut("/orders/{id}", (Guid id, OrderAddRequest addRequest) =>
  {
    var order = new OrderEntity
    {
      Id = id,
      Name = addRequest.Name,
      Cost = addRequest.Cost
    };

    Database.Orders[id] = order;
  })
  .AddEndpointFilter<HttpExceptionEndPointFilter>();

app.Run();