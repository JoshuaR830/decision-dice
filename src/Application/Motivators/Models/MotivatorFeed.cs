namespace Application.Motivators.Models;
public record MotivatorFeed(IEnumerable<Motivator> MotivatorList, string Category, string UserName);