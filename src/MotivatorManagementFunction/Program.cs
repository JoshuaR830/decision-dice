//using Amazon.Lambda.APIGatewayEvents;
//using Amazon.Lambda.Core;
//using Amazon.Lambda.RuntimeSupport;
//using Amazon.Lambda.Serialization.SystemTextJson;
//using Amazon.S3;
//using decision_dice.Models;

using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using decision_dice.Models;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddApplicationServices();
builder.Services.AddAWSLambdaHosting(LambdaEventSource.HttpApi);
var app = builder.ConfigureLambdaApplication();

app.MapPost("/", ([FromBody]Motivator motivator) =>
{
    //var motivator = JsonSerializer.Deserialize<Motivator>(input.Body);

    if (motivator == null)
    {
        Console.WriteLine("Failed to serialize body");
        return "No motivator could be retrieved";
    }

    Console.WriteLine($"New motivator for {motivator.UserName}, called {motivator.Title}");

    return $"New motivator for {motivator.UserName}, called {motivator.Title}";
});

app.Run();

//var s3Client = new AmazonS3Client();

// A URL lambda still takes APIGatewayHttpApiV2ProxyRequest
//var handler = async (APIGatewayHttpApiV2ProxyRequest input, ILambdaContext context) =>
//{

//    var motivator = JsonSerializer.Deserialize<Motivator>(input.Body);

//    if (motivator == null)
//    {
//        Console.WriteLine("Failed to serialize body");
//        return "No motivator could be retrieved";
//    }

//    Console.WriteLine($"New motivator for {motivator.UserName}, called {motivator.Title}");

//    return $"New motivator for {motivator.UserName}, called {motivator.Title}";
//};

//await LambdaBootstrapBuilder.Create(handler, new DefaultLambdaJsonSerializer())
//    .Build()
//    .RunAsync();