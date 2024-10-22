namespace Application;


public class Result
{
    private Result()
    {
        Error = null;
    }

    private Result(Error error)
    {
        Error = error;
    }
    
    public Error? Error;
    public bool IsSuccess => Error is null;
    
    public static Result Success() => new();
    public static Result Failure(Error error) => new(error);
    
    public TResult Match<TResult>(Func<TResult> onSuccess, Func<Error, TResult> onFailure)
    {
        return IsSuccess ? onSuccess() : onFailure(Error!);
    }
    
}

public class Result<T>
{
    private Result(T value)
    {
        Value = value;
        Error = null;
    }

    private Result(Error error)
    {
        Error = error;
        Value = default;
    }
    
    public T? Value;
    public Error? Error;
    public bool IsSuccess => Error is null;
    
    public static Result<T> Success(T value) => new(value);
    public static Result<T> Failure(Error error) => new(error);
    
    public TResult Match<TResult>(Func<T, TResult> onSuccess, Func<Error, TResult> onFailure)
    {
        return IsSuccess ? onSuccess(Value!) : onFailure(Error!);
    }
}