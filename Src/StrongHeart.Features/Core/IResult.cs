namespace StrongHeart.Features.Core;

public interface IResult
{
    ResultType Status { get; }
    string? Error { get; }

    bool IsSuccess { get; }
}