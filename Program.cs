using System.Reflection;
using HelloWorld.Data;
using HelloWorld.Dto.Orders;
using HelloWorld.Entities;
using HelloWorld.Exceptions;
using HelloWorld.Filters;
using HelloWorld.Services;
using HelloWorld.Services.Senders;
using HelloWorld.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;

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
builder.Services.Configure<VkIntegration>(builder.Configuration);

//builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
  options.SwaggerDoc("v1", new OpenApiInfo()
  {
    Version = "v1",
    Title = "Itis example API",
    Description = "An ASP.NET Core Web API application",
    TermsOfService = new Uri("https://example.com/terms"),
    Contact = new OpenApiContact
    {
      Name = "Example Contact",
      Url = new Uri("https://example.com/contact")
    },
    License = new OpenApiLicense
    {
      Name = "Example License",
      Url = new Uri("https://example.com/license")
    }
  });
  
  var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
  options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

builder.Services.AddDbContext<ApplicationDbContext>(options =>
  options.UseNpgsql(builder.Configuration.GetConnectionString("ItisDatabase")));
builder.Services.AddDbContext<OrdersDbContext>(options =>
  options.UseNpgsql(builder.Configuration.GetConnectionString("ItisDatabase")));


builder.Services.AddMvc(
  options => 
    options.Filters.Add<HttpExceptionFilter>());


var app = builder.Build();
app.UseSwaggerUI();
app.UseSwagger();

//app.UseMiddleware<HttpExceptionHandlerMiddleware>();
app.MapControllers();

app.MapGet("/", () => "Hello World!")
.AddEndpointFilter<HttpExceptionEndPointFilter>();



app.MapGet("/orders", (IDataBase dataBase, IOptions<VkIntegration> vkConfig) => vkConfig.Value.ApiKey + " " + vkConfig.Value.Retry)
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