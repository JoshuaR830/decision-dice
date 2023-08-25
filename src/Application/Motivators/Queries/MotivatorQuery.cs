namespace Application.Queries;

public sealed class MotivatorQuery: IRequest<Motivator>
{
    internal sealed class Handler : IRequestHandler<MotivatorQuery, Motivator>
    {
        public Task<Motivator> Handle(MotivatorQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(new Motivator(Guid.NewGuid(), "Title", "Description", "", ""));
        }
    }
}
