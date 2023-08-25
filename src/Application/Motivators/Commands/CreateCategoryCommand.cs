namespace Application.Motivators.Commands;

public class CreateCategoryCommand : IRequest
{
    public readonly Category _category;

    public CreateCategoryCommand(Category category)
    {
        _category = category;
    }

    internal class Handler : IRequestHandler<CreateCategoryCommand>
    {
        IAmazonS3 _s3Client;

        public Handler(IAmazonS3 s3Client) =>
            _s3Client = s3Client;

        public async Task Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var key = request._category.GenerateIdentifier();
            var content = request._category.Serialize();

            await _s3Client.PutObjectAsync(new PutObjectRequest
            {
                BucketName = IdentifierExtensions.BUCKET_NAME,
                Key = key,
                ContentType = "application/json",
                ContentBody = content,
            });
        }
    }
}
