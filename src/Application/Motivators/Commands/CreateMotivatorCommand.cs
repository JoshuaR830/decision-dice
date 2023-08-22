using decision_dice.Models;

namespace decision_dice.Commands;

public class CreateMotivatorCommand : IRequest
{
    protected Motivator _motivator;

    public CreateMotivatorCommand(Motivator motivator)
    {
        _motivator = motivator;
    }

    internal sealed class Handler : IRequestHandler<CreateMotivatorCommand>
    {
        public Task Handle(CreateMotivatorCommand request, CancellationToken cancellationToken)
        {
            var key = request._motivator.GenerateIdentifier();
            var content = request._motivator.Serialize();

            Console.WriteLine($"New motivator for {key}, called {content}");

            return Task.CompletedTask;
        }
    }
}
