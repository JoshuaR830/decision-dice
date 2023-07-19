using decision_dice.Models;
using MediatR;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using System.Text.Json;

namespace decision_dice.Queries;

public sealed class MotivatorQuery: IRequest<Motivator>
{
    internal sealed class Handler : IRequestHandler<MotivatorQuery, Motivator>
    {
        IAmazonDynamoDB _dynamoDb;

        public Handler(IAmazonDynamoDB dynamoDb)
        {
            this._dynamoDb = dynamoDb;
        }

        public async Task<Motivator> Handle(MotivatorQuery request, CancellationToken cancellationToken)
        {
            // ToDo - make this better
            var result = await _dynamoDb.GetItemAsync(new GetItemRequest());

            var dataKey = "Data";

            if (!result.IsItemSet || !result.Item.ContainsKey(dataKey))
                throw new Exception();

            var motivator = JsonSerializer.Deserialize<Motivator>(result.Item[dataKey].S);

            if (motivator == null)
                throw new Exception();

            return motivator;
        }
    }
}
