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
        IAWSHelper _awsHelper;

        public Handler(IAWSHelper awsHelper) =>
            _awsHelper = awsHelper;

        public async Task Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var key = request._category.GenerateIdentifier();
            var content = request._category.Serialize();

            await _awsHelper.PutObject(key, content);
            await _awsHelper.InvalidateCache(key);
        }
    }
}
