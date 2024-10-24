namespace Application;

public interface IResult
{
    public Error? Error { get; init; }
    public bool IsSuccess => Error is null;
}