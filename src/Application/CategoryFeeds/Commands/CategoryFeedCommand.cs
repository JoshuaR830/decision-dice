namespace Application.CategoryFeeds.Commands;

public class CategoryFeedCommand : IRequest
{
    public readonly Category _category;

    public CategoryFeedCommand(Category category) =>
        _category = category;

    internal class Handler : IRequestHandler<CategoryFeedCommand>
    {
        readonly IMediator _mediator;
        readonly IAWSHelper _awsHelper;

        public Handler(IAmazonS3 s3Client, IAmazonCloudFront cloudfrontClient, IMediator mediator, IAWSHelper awsHelper)
        {
            _mediator = mediator;
            _awsHelper = awsHelper;
        }

        public async Task Handle(CategoryFeedCommand request, CancellationToken cancellationToken)
        {
            var existingFeed = await _mediator.Send(new CategoryFeedQuery(request._category.UserName));

            if (!existingFeed.Categories.Any(name => name == request._category.CategoryName))
                existingFeed.Categories.Add(request._category.CategoryName);

            var key = $"feeds/category/{request._category.UserName}";
            await _awsHelper.PutObject(key, existingFeed);
            await _awsHelper.InvalidateCache(key);
        }
    }
}

