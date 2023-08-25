using Microsoft.AspNetCore.Mvc;
using Amazon.S3;
using Amazon.Extensions.NETCore.Setup;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddApplicationServices();
builder.Services.AddAWSLambdaHosting(LambdaEventSource.HttpApi);
builder.Services.AddDefaultAWSOptions(new AWSOptions());
builder.Services.AddAWSService<IAmazonS3>();
var app = builder.ConfigureLambdaApplication();

app.MapPost("/motivator", async ([FromBody]Motivator motivator, IMediator mediator) =>
{
    if (motivator == null)
        return "No motivator could be retrieved";
 
    await mediator.Send(new CreateMotivatorCommand(motivator));

    return $"New motivator for {motivator.UserName}, called {motivator.Title}";
});

app.MapPost("/category", async ([FromBody] Category category, IMediator mediator) =>
{
    if (category == null)
        return "No category could be retrieved";

    await mediator.Send(new CreateCategoryCommand(category));

    return $"New category for {category.UserName}, called {category.CategoryName}";
});

app.Run();