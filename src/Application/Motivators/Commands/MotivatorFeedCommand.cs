namespace Application.Motivators.Commands;

public class MotivatorFeedCommand : IRequest
{
    public Motivator _motivator;

    public MotivatorFeedCommand(Motivator motivator) =>
        _motivator = motivator;
 
    internal class Handler : IRequestHandler<MotivatorFeedCommand>
    {
        public readonly IAmazonS3 _s3Client;
        public readonly IMediator _mediator;

        public Handler(IAmazonS3 s3Client, IMediator mediator)
        {
            _s3Client = s3Client;
            _mediator = mediator;
        }

        public async Task Handle(MotivatorFeedCommand request, CancellationToken cancellationToken)
        {
            var motivatorFeed = await _mediator.Send(new MotivatorFeedQuery(request._motivator.Category, request._motivator.UserName));

            if(!motivatorFeed.MotivatorList.Any(title => title == request._motivator.Title))
                motivatorFeed.MotivatorList.Add(request._motivator.Title);

            await _s3Client.PutObjectAsync(new PutObjectRequest
            {
                BucketName = IdentifierExtensions.BUCKET_NAME,
                Key = $"feeds/motivator/{request._motivator.UserName}/{request._motivator.Category}",
                ContentType = "application/json",
                ContentBody = motivatorFeed.Serialize()
            });
        }
    }
}

