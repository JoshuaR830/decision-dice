namespace Application.Motivators.Commands;

public class CategoryFeedCommand : IRequest
{
    public readonly Category _category;

    public CategoryFeedCommand(Category category) =>
        _category = category;

    internal class Handler : IRequestHandler<CategoryFeedCommand>
    {
        readonly IAmazonS3 _s3Client;
        readonly IMediator _mediator;

        public Handler(IAmazonS3 s3Client, IMediator mediator)
        {
            _s3Client = s3Client;
            _mediator = mediator;
        }

        public async Task Handle(CategoryFeedCommand request, CancellationToken cancellationToken)
        {
            var existingFeed = await _mediator.Send(new CategoryFeedQuery(request._category.UserName));
            existingFeed.Categories.Add(request._category);

            Console.WriteLine(request._category);
            Console.WriteLine(existingFeed.Serialize());

            await _s3Client.PutObjectAsync(new PutObjectRequest
            {
                BucketName = IdentifierExtensions.BUCKET_NAME,
                Key = $"feeds/category/{request._category.UserName}",
                ContentType = "application/json",
                ContentBody = existingFeed.Serialize()
            });
        }
    }
}

