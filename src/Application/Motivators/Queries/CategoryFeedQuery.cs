namespace Application.Queries;

public sealed class CategoryFeedQuery: IRequest<CategoryFeed>
{
    public string _userName;

    public CategoryFeedQuery(string userName)
    {
        _userName = userName;
    }

    internal sealed class Handler : IRequestHandler<CategoryFeedQuery, CategoryFeed>
    {
        public readonly IAmazonS3 _s3Client;

        public Handler(IAmazonS3 s3Client) => _s3Client = s3Client;

        public async Task<CategoryFeed> Handle(CategoryFeedQuery request, CancellationToken cancellationToken)
        {
            var feed = await _s3Client.GetObjectAsync(new GetObjectRequest
            {
                BucketName = IdentifierExtensions.BUCKET_NAME,
                Key = $"feeds/category/{request._userName}",
            });

            if (feed.HttpStatusCode != System.Net.HttpStatusCode.OK)
                return null;

            using var stream = feed.ResponseStream;
            using var reader = new StreamReader(stream);
            var response = await reader.ReadToEndAsync();

            CategoryFeed responseObject;

            try
            {
                responseObject = response.Deserialize<CategoryFeed>();
            }
            catch (Exception)
            {
                responseObject = new CategoryFeed(Enumerable.Empty<Category>(), request._userName);
            }

            return responseObject;
        }
    }
}
