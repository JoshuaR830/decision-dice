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
        private readonly IAWSHelper _awsHelper;

        public Handler(IAWSHelper awsHelper) =>
            _awsHelper = awsHelper;

        public async Task<CategoryFeed> Handle(CategoryFeedQuery request, CancellationToken cancellationToken)
        {
            var feed = await _awsHelper.GetObject<CategoryFeed>($"feeds/category/{request._userName}");

            if (feed.IsError || feed.Success == null)
                return new CategoryFeed(new List<string>(), request._userName);

            return feed.Success;
        }
    }
}
