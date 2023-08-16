namespace decision_dice.ExtensionMethods
{
    public static class SerializerExtensions
    {
        public static string Serialize<T>(this T model)
        {
            var options = new JsonSerializerOptions { WriteIndented = true, PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
            return JsonSerializer.Serialize(model, options);
        }
    }
}
