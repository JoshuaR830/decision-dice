using decision_dice.Commands;
using decision_dice.Models;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddApplicationServices();
builder.Services.AddAWSLambdaHosting(LambdaEventSource.HttpApi);
var app = builder.ConfigureLambdaApplication();

app.MapPost("/", async ([FromBody]Motivator motivator, IMediator mediator) =>
{
    if (motivator == null)
    {
        Console.WriteLine("Failed to serialize body");
        return "No motivator could be retrieved";
    }

    await mediator.Send(new CreateMotivatorCommand(motivator));

    return $"New motivator for {motivator.UserName}, called {motivator.Title}";
});

app.Run();