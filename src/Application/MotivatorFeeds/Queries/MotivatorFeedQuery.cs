namespace Application.MotivatorFeeds.Queries;

public sealed class MotivatorFeedQuery : IRequest<MotivatorFeed>
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
        public readonly IAWSHelper _awsHelper;

        public Handler(IAWSHelper awsHelper) => _awsHelper = awsHelper;

        public async Task<MotivatorFeed> Handle(MotivatorFeedQuery request, CancellationToken cancellationToken)
        {
            var feed = await _awsHelper.GetObject<MotivatorFeed>($"feeds/motivator/{request._userName}/{request._categoryName}");

            if (feed.IsError || feed.Success == null)
                return new MotivatorFeed(new List<string>(), request._categoryName, request._userName);

            return feed.Success;
        }
    }
}
