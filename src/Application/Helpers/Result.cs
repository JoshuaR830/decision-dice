namespace Application.Motivators.Models;

public class Result<T>
{
    public bool IsSuccess { get; set; }
    public bool IsError => !IsSuccess;
    public T? Success { get; set; }
    public string? Error{ get; set; }
    
    public Result(string errorMessage)
    {
        this.IsSuccess = false;
        this.Error = errorMessage;
    }

    public Result(T successResult)
    {
        IsSuccess = true;
        Success = successResult;
    }
}
