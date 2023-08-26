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
            MotivatorFeed responseObject;
            Console.WriteLine("Motivator Feed Query");
            try
            {
                var feed = await _s3Client.GetObjectAsync(new GetObjectRequest
                {
                    BucketName = IdentifierExtensions.BUCKET_NAME,
                    Key = $"feeds/motivator/{request._userName}/{request._categoryName}",
                });

                Console.WriteLine("S3 request made");

                using var stream = feed.ResponseStream;
                using var reader = new StreamReader(stream);
                var response = await reader.ReadToEndAsync();

                responseObject = response.Deserialize<MotivatorFeed>();
                Console.WriteLine("Successfully serialized");
            }
            catch (Exception)
            {
                responseObject = new MotivatorFeed(new List<Motivator>(), request._categoryName, request._userName);
            }

            Console.WriteLine(responseObject.Serialize());
            return responseObject;
        }
    }
}
