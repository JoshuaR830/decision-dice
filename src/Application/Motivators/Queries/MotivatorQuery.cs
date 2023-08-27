namespace Application.Queries;

public sealed class MotivatorQuery: IRequest<Motivator?>
{
    private string _userName;
    private string _categoryName;
    private string _title;

    public MotivatorQuery(string userName, string categoryName, string title)
    {
        _userName = userName;
        _categoryName = categoryName;
        _title = title;
    }

    internal sealed class Handler : IRequestHandler<MotivatorQuery, Motivator?>
    {
        private readonly IAWSHelper _awsHelper;

        public Handler(IAWSHelper awsHelper) =>
            _awsHelper = awsHelper;

        public async Task<Motivator?> Handle(MotivatorQuery request, CancellationToken cancellationToken)
        {
            var motivator = await _awsHelper.GetObject<Motivator>($"motivators/{request._userName}/{request._categoryName}/{request._title}");
            Console.WriteLine(motivator.Serialize());
            if (motivator.IsError || motivator.Success == null)
                return null;
            
            return motivator.Success;
        }
    }
}
