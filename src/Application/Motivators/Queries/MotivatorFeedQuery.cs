namespace Application.Queries;

public sealed class MotivatorFeedQuery: IRequest<MotivatorFeed>
{
    public string _categoryName;
    public string _userName;

    public MotivatorFeedQuery(string categoryName, string userName)
    {
        _categoryName = categoryName;
        _userName = userName;
    }

    internal sealed class Handler : IRequestHandler<MotivatorFeedQuery, MotivatorFeed>
    {
        public readonly IAmazonS3 _s3Client;

        public Handler(IAmazonS3 s3Client) => _s3Client = s3Client;

        public async Task<MotivatorFeed> Handle(MotivatorFeedQuery request, CancellationToken cancellationToken)
        {
            var feed = await _s3Client.GetObjectAsync(new GetObjectRequest
            {
                BucketName = IdentifierExtensions.BUCKET_NAME,
                Key = $"feeds/motivators/{request._userName}/{request._categoryName}",
            });

            using var stream = feed.ResponseStream;
            using var reader = new StreamReader(stream);
            var response = await reader.ReadToEndAsync();

            MotivatorFeed responseObject;

            try
            {
                responseObject = response.Deserialize<MotivatorFeed>();
            }
            catch (Exception)
            {
                responseObject = new MotivatorFeed(Enumerable.Empty<Motivator>(), request._categoryName, request._userName);
            }

            return responseObject;
        }
    }
}
