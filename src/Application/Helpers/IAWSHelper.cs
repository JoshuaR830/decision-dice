namespace Application.Helpers
{
    public interface IAWSHelper
    {
        Task PutObject(string key, object content);
        Task InvalidateCache(string key);
    }
}
