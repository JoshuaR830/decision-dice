namespace Application.Motivators.Commands;

public class CreateMotivatorCommand : IRequest
{
    protected Motivator _motivator;

    public CreateMotivatorCommand(Motivator motivator) =>
        _motivator = motivator;

    internal sealed class Handler : IRequestHandler<CreateMotivatorCommand>
    {
        private readonly IAWSHelper _awsHelper;

        public Handler(IAWSHelper awsHelper) =>
            _awsHelper = awsHelper;

        public async Task Handle(CreateMotivatorCommand request, CancellationToken cancellationToken)
        {
            var key = request._motivator.GenerateIdentifier();
            var content = request._motivator.Serialize();

            await _awsHelper.PutObject(key, content);
            await _awsHelper.InvalidateCache(key);
        }
    }
}
