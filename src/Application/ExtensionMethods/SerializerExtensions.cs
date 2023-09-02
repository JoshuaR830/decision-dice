namespace Application.ExtensionMethods;
public static class SerializerExtensions
{
    public static string Serialize<T>(this T model)
    {
        var options = new JsonSerializerOptions { WriteIndented = true, PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
        return JsonSerializer.Serialize(model, options);
    }

    public static T Deserialize<T>(this string jsonString)
    {
        var options = new JsonSerializerOptions { WriteIndented = true, PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

        return JsonSerializer.Deserialize<T>(jsonString, options);
    }
}
