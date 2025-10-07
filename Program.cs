using HelloWorld.Dto.Orders;
using HelloWorld.Entities;
using HelloWorld.Exceptions;
using HelloWorld.Filters;
using HelloWorld.Services;
using HelloWorld.Services.Senders;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddSingleton<IDataBase, Database>();

builder.Services.AddScoped<ICitiesUpdater, CitiesService>();
builder.Services.AddScoped<ICitiesProvider, CitiesService>();

// builder.Services.AddKeyedScoped<IMessageSender, EmailSender>("Email");
// builder.Services.AddKeyedScoped<IMessageSender, SmsSender>("Sms");
// builder.Services.AddKeyedScoped<IMessageSender, TelegramSender>("Telegram");


builder.Services.AddScoped<IMessageSender, EmailSender>();
builder.Services.AddScoped<IMessageSender, SmsSender>();
builder.Services.AddScoped<IMessageSender, TelegramSender>();

builder.Services.AddMvc(
  options => 
    options.Filters.Add<HttpExceptionFilter>());

var app = builder.Build();

//app.UseMiddleware<HttpExceptionHandlerMiddleware>();
app.MapControllers();

app.MapGet("/", () => "Hello World!")
.AddEndpointFilter<HttpExceptionEndPointFilter>();



app.MapGet("/orders", (IDataBase dataBase) => dataBase.Orders.Values)
.AddEndpointFilter<HttpExceptionEndPointFilter>();

app.MapGet("/orders/{id}", (Guid id, IDataBase dataBase) =>
  {
    if (!dataBase.Orders.ContainsKey(id))
      throw new NotFoundException($"Order with id: {id} was not found");
    
    var order = dataBase.Orders[id];

    return new OrderDto
    {
      Id = order.Id,
      Name = order.Name,
      Cost = order.Cost
    };
  })
.AddEndpointFilter<HttpExceptionEndPointFilter>();


app.MapDelete("/orders/{id}", (Guid id, IDataBase dataBase) =>
  {
    if (!dataBase.Orders.ContainsKey(id))
      throw new NotFoundException($"Order with id: {id} was not found");

    dataBase.Orders.Remove(id);
  })
  .AddEndpointFilter<HttpExceptionEndPointFilter>();

app.MapPost("/orders", (OrderAddRequest addRequest, IDataBase dataBase) =>
  {
    var newId = Guid.NewGuid();
    
    dataBase.Orders.Add(newId, new OrderEntity
    {
      Id = newId,
      Name = addRequest.Name,
      Cost = addRequest.Cost
    });  
  })
  .AddEndpointFilter<HttpExceptionEndPointFilter>();

app.MapPut("/orders/{id}", (Guid id, OrderAddRequest addRequest, IDataBase dataBase) =>
  {
    var order = new OrderEntity
    {
      Id = id,
      Name = addRequest.Name,
      Cost = addRequest.Cost
    };

    dataBase.Orders[id] = order;
  })
  .AddEndpointFilter<HttpExceptionEndPointFilter>();

app.Run();