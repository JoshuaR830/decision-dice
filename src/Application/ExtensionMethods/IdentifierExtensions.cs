namespace decision_dice.ExtensionMethods
{
    public static class IdentifierExtensions
    {
        public static string GenerateIdentifier(this Motivator motivator) =>
            $"MOTIVATOR#{motivator.UserName}#{motivator.Category}";
    }
}
