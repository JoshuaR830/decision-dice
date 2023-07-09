using MediatR;

namespace decision_dice.Motivators
{
    public class MotivatorQueryHandler : IRequestHandler<MotivatorQuery, Motivator>
    {
        public Task<Motivator> Handle(MotivatorQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(new Motivator(Guid.NewGuid(), "Title", "Description"));
        }
    }
}
