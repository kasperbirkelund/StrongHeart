using System;

namespace StrongHeart.Features.Core;

public class Result<T> : IResult
{
    public bool IsFailure => Status == ResultType.ClientError || Status == ResultType.ServerError;
    public bool IsSuccess => !IsFailure;
    public ResultType Status { get; }
    public string? Error { get; }
    private readonly T _value;
    public T Value => IsSuccess ? _value : throw new InvalidOperationException(Error);

    private Result(string? error, T value, ResultType status)
    {
        _value = value;
        Status = status;
        Error = error;
    }

    public static Result<T> Success(T value)
    {
        return new(default, value, ResultType.ExecutedSuccessfully);
    }

    public static Result<T> ClientError(string error)
    {
        return new(error, default, ResultType.ClientError);
    }

    public static Result<T> ServerError(string error)
    {
        return new(error, default, ResultType.ServerError);
    }

    public static Result<T> QueuedForLaterExecution(T value)
    {
        return new(default, value, ResultType.QueuedForLaterExecution);
    }

    public static Result<T> FromResult<T>(Result result, T args)
    {
        return result.Status switch
        {
            ResultType.ExecutedSuccessfully => Result<T>.Success(args),
            ResultType.QueuedForLaterExecution => Result<T>.QueuedForLaterExecution(args),
            ResultType.ClientError => Result<T>.ClientError(result.Error!),
            ResultType.ServerError => Result<T>.ServerError(result.Error!),
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    public T ExtractSuccessful()
    {
        if (IsSuccess)
        {
            return Value;
        }
        throw new Exception("State is not successful");
    }
}

public class Result : IResult, IRequestDto
{
    public bool IsFailure => Status == ResultType.ClientError || Status == ResultType.ServerError;
    public bool IsSuccess => !IsFailure;
    public ResultType Status { get; }
    public string? Error { get; }

    private Result(string? error, ResultType status)
    {
        Error = error;
        Status = status;
    }

    public static Result Success()
    {
        return new(default, ResultType.ExecutedSuccessfully);
    }

    public static Result ClientError(string error)
    {
        return new(error, ResultType.ClientError);
    }

    public static Result ServerError(string error)
    {
        return new(error, ResultType.ServerError);
    }

    public static Result QueuedForLaterExecution()
    {
        return new(default, ResultType.QueuedForLaterExecution);
    }
}