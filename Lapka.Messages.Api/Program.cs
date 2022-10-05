using System.Reflection;
using System.Text.Json.Serialization;
using Convey;
using Convey.MessageBrokers.CQRS;
using Convey.MessageBrokers.RabbitMQ;
using Lapka.Messages.Application;
using Lapka.Messages.Application.ExternalEvents;
using Lapka.Messages.Infrastructure;
using Lapka.Messages.Infrastructure.Jwt;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddControllers()
    .AddJsonOptions(options => options.JsonSerializerOptions.WriteIndented = true)
    .AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerConfiguration();
builder.Services.AddSwaggerGen();
builder.Services.AddAuth(builder.Configuration);

builder.Services.Configure<RouteOptions>(options => options.LowercaseUrls = true);

var app = builder.Build();

app.UseConvey();
app.UseRabbitMq()
    .SubscribeEvent<UserDeletedEvent>()
    .SubscribeEvent<UserUpdatedEvent>()
    .SubscribeEvent<UserCreatedEvent>();

app.UseSwaggerDocs();

app.UseMiddleware();

app.UseAuthentication();

app.UseAuthorization();

app.MapGet("/", ctx => ctx.Response.WriteAsync($"Lapka.Messages API {DateTime.Now}"));

app.MapControllers();
app.Run();