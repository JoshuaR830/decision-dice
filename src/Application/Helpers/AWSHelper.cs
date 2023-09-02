namespace Application.Helpers;

public class AWSHelper : IAWSHelper
{
    IAmazonS3 _s3Client;
    IAmazonCloudFront _cloudFrontClient;

    public AWSHelper(IAmazonS3 s3Client, IAmazonCloudFront cloudFrontClient)
    {
        _s3Client = s3Client;
        _cloudFrontClient = cloudFrontClient;
    }
    public async Task PutObject(string key, object content)
    {
        await _s3Client.PutObjectAsync(new PutObjectRequest
        {
            BucketName = IdentifierExtensions.BUCKET_NAME,
            Key = key.ReplaceSpacesWithDashes(),
            ContentType = "application/json",
            ContentBody = content.Serialize()
        });
    }

    async Task<Result<T>> IAWSHelper.GetObject<T>(string key)
    {
        Result<T> responseObject;

        try
        {
            var s3Response = await _s3Client.GetObjectAsync(new GetObjectRequest
            {
                BucketName = IdentifierExtensions.BUCKET_NAME,
                Key = key.ReplaceSpacesWithDashes()
            });

            if (s3Response.HttpStatusCode != System.Net.HttpStatusCode.OK)
                return new Result<T>($"Request to S3 returned {s3Response.HttpStatusCode}");

            using var stream = s3Response.ResponseStream;
            using var reader = new StreamReader(stream);
            var response = await reader.ReadToEndAsync();

            responseObject = new Result<T>(response.Deserialize<T>());
        }
        catch (Exception e)
        {
            responseObject = new Result<T>(e.Message);
        }

        return responseObject;
    }

    public async Task InvalidateCache(string key)
    {
        await _cloudFrontClient.CreateInvalidationAsync(new CreateInvalidationRequest
        {
            DistributionId = "E33YSSGHUP7B9Q",
            InvalidationBatch = new InvalidationBatch
            {
                CallerReference = DateTime.UtcNow.Ticks.ToString(),
                Paths = new Paths
                {
                    Items = new List<string> { $"/{key.ReplaceSpacesWithDashes()}" },
                    Quantity = 1
                }
            }
        });
    }
}
