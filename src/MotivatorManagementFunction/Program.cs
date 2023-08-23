using decision_dice.Commands;
using decision_dice.Models;
using Microsoft.AspNetCore.Mvc;

using Amazon.S3;
using Amazon.Extensions.NETCore.Setup;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddApplicationServices();
builder.Services.AddAWSLambdaHosting(LambdaEventSource.HttpApi);
builder.Services.AddDefaultAWSOptions(new AWSOptions());
builder.Services.AddAWSService<IAmazonS3>();
var app = builder.ConfigureLambdaApplication();

app.MapPost("/", async ([FromBody]Motivator motivator, IMediator mediator) =>
{
    if (motivator == null)
        return "No motivator could be retrieved";
 
    await mediator.Send(new CreateMotivatorCommand(motivator));

    return $"New motivator for {motivator.UserName}, called {motivator.Title}";
});

app.Run();