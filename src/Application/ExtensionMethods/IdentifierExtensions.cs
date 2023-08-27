namespace Application.ExtensionMethods;
public static class IdentifierExtensions
{
    public const string BUCKET_NAME = "decision-dice-motivators";
    public static string GenerateIdentifier(this Motivator motivator) =>
        $"motivators/{motivator.UserName}/{motivator.Category}/{motivator.Title}".ReplaceSpacesWithDashes();

    public static string GenerateIdentifier(this Category category) =>
        $"categories/{category.UserName}/{category.CategoryName}".ReplaceSpacesWithDashes();

    public static string ReplaceSpacesWithDashes(this string input)
    {
        return input.Replace(' ', '-');    
    }
}
