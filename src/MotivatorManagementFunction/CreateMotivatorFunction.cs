using System.Net;

using Amazon.Lambda.Core;
using Amazon.Lambda.RuntimeSupport;
using Amazon.Lambda.Serialization.SystemTextJson;
using Amazon.S3;
using Amazon.Lambda.S3Events;

using decision_dice.Models;
using System.Text.Json.Serialization;

//[assembly: LambdaSerializer(typeof(SourceGeneratorLambdaJsonSerializer<HttpApiJsonSerializerContext>))]

namespace Management;

var s3Client = new AmazonS3Client();
// Need to add the types that will be serialized for faster responses
//[JsonSerializable(typeof(Motivator))]
//public partial class HttpApiJsonSerializerContext : JsonSerializerContext { }

// https://aws.amazon.com/blogs/compute/introducing-the-net-6-runtime-for-aws-lambda/#:~:text=NET%206%20Lambda%20runtime%20adds,NET%20project.

var handler = async (S3Event evnt, ILambdaContext context) =>
{
    foreach(var record in evnt.Records)
    {
        using var response = await s3Client.GetObjectAsync(record.S3.Bucket.Name, record.S3.Object.Key);
        using ver reader = new StreamReader(response.ResponseStream);
    }
};

await LambdaBootstrapBuilder.Create(handler, new DefaultLambdaJsonSerializer())
    .Build()
    .RunAsync();

