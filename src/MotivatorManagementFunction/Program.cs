using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using Amazon.Lambda.RuntimeSupport;
using Amazon.Lambda.Serialization.SystemTextJson;
using Amazon.S3;
using decision_dice.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddApplicationServices();
builder.Services.AddAWSLambdaHosting(LambdaEventSource.HttpApi);
builder.ConfigureApplication();

var s3Client = new AmazonS3Client();

// A URL lambda still takes APIGatewayHttpApiV2ProxyRequest
var handler = async (APIGatewayHttpApiV2ProxyRequest input, ILambdaContext context) =>
{
    var motivator = JsonSerializer.Deserialize<Motivator>(input.Body);

    LambdaLogger.Log($"New motivator for {motivator.UserName}, callled {motivator.Title}");

    return $"New motivator for {motivator.UserName}, callled {motivator.Title}";
};

await LambdaBootstrapBuilder.Create(handler, new DefaultLambdaJsonSerializer())
    .Build()
    .RunAsync();