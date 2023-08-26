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

    public async Task InvalidateCache(string key)
    {
        await _cloudFrontClient.CreateInvalidationAsync(new CreateInvalidationRequest
        {
            DistributionId = "E33YSSGHUP7B9Q",
            InvalidationBatch = new InvalidationBatch
            {
                Paths = new Paths
                {
                    Items = new List<string> { $"/{key}" },
                    Quantity = 1
                }
            }
        });
    }
}
