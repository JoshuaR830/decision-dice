namespace Application.Helpers;

public interface IAWSHelper
{
    Task<Result<T>> GetObject<T>(string key);
    Task PutObject(string key, object content);
    Task InvalidateCache(string key);
}
