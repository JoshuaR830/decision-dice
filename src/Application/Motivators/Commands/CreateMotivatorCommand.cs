namespace Application.Motivators.Commands;

public class CreateMotivatorCommand : IRequest
{
    protected Motivator _motivator;

    public CreateMotivatorCommand(Motivator motivator) =>
        _motivator = motivator;

    internal sealed class Handler : IRequestHandler<CreateMotivatorCommand>
    {
        private readonly IAmazonS3 _s3Client;

        public Handler(IAmazonS3 srClient) => _s3Client = srClient;

        public async Task Handle(CreateMotivatorCommand request, CancellationToken cancellationToken)
        {
            var key = request._motivator.GenerateIdentifier();
            var content = request._motivator.Serialize();

            Console.WriteLine($"New motivator for {key}, called {content}");

            await _s3Client.PutObjectAsync(new PutObjectRequest
            {
                BucketName = IdentifierExtensions.BUCKET_NAME,
                Key = key,
                ContentType = "application/json",
                ContentBody = content
            }, cancellationToken);
        }
    }
}
