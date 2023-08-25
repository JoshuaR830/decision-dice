namespace Application.ExtensionMethods;
public static class IdentifierExtensions
{
    public const string BUCKET_NAME = "decision-dice-motivators";
    public static string GenerateIdentifier(this Motivator motivator) =>
        $"motivators/{motivator.UserName}/{motivator.Category}";

    public static string GenerateIdentifier(this Category category) =>
        $"categories/{category.UserName}/{category.CategoryName}";
}
