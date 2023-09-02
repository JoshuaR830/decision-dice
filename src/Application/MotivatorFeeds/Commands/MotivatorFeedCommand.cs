namespace Application.MotivatorFeeds.Commands;

public class MotivatorFeedCommand : IRequest
{
    public Motivator _motivator;

    public MotivatorFeedCommand(Motivator motivator) =>
        _motivator = motivator;

    internal class Handler : IRequestHandler<MotivatorFeedCommand>
    {
        public readonly IAWSHelper _awsHelper;
        public readonly IMediator _mediator;

        public Handler(IAWSHelper awsHelper, IMediator mediator)
        {
            _mediator = mediator;
            _awsHelper = awsHelper;
        }

        public async Task Handle(MotivatorFeedCommand request, CancellationToken cancellationToken)
        {
            var motivatorFeed = await _mediator.Send(new MotivatorFeedQuery(request._motivator.Category, request._motivator.UserName));

            if (!motivatorFeed.Motivators.Any(title => title == request._motivator.Title))
                motivatorFeed.Motivators.Add(request._motivator.Title);

            var key = $"feeds/motivator/{request._motivator.UserName}/{request._motivator.Category}";
            await _awsHelper.PutObject(key, motivatorFeed);
            await _awsHelper.InvalidateCache(key);
        }
    }
}

