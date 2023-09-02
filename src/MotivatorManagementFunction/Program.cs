using Microsoft.AspNetCore.Mvc;
using Amazon.S3;
using Amazon.Extensions.NETCore.Setup;
using Amazon.CloudFront;
using Application.Categories.Models;
using Application.MotivatorFeeds.Commands;
using Application.Categories.Commands;
using Application.CategoryFeeds.Commands;
using Application.CategoryFeeds.Queries;
using Application.MotivatorFeeds.Queries;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddApplicationServices();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAWSLambdaHosting(LambdaEventSource.HttpApi);
builder.Services.AddDefaultAWSOptions(new AWSOptions());
builder.Services.AddAWSService<IAmazonS3>();
builder.Services.AddAWSService<IAmazonCloudFront>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapPost("/motivator", async ([FromBody]Motivator motivator, IMediator mediator) =>
{
    if (motivator == null)
        return "No motivator could be retrieved";
 
    await mediator.Send(new CreateMotivatorCommand(motivator));
    await mediator.Send(new MotivatorFeedCommand(motivator));

    return $"New motivator for {motivator.UserName}, called {motivator.Title}";
});

app.MapPost("/category", async ([FromBody] Category category, IMediator mediator) =>
{
    if (category == null)
        return "No category could be retrieved";

    await mediator.Send(new CreateCategoryCommand(category));
    await mediator.Send(new CategoryFeedCommand(category));

    return $"New category for {category.UserName}, called {category.CategoryName}";
});

app.MapGet("/category-feed/{userName}", async (string userName, IMediator mediator) =>
{
    var feed = await mediator.Send(new CategoryFeedQuery(userName));
    return feed;
});

app.MapGet("/motivator-feed/{userName}/{categoryName}", async (string userName, string categoryName, IMediator mediator) =>
{
    var feed = await mediator.Send(new MotivatorFeedQuery(categoryName, userName));
    return feed;
});

app.MapGet("/motivator/{userName}/{categoryName}/{title}", async (string userName, string categoryName, string title, IMediator mediator) =>
{
    var motivator = await mediator.Send(new MotivatorQuery(userName, categoryName, title));
    return motivator;
});

app.Run();