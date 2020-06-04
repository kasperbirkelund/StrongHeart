using System;

namespace StrongHeart.Features.Core
{
    public class Result<T> : IResult
    {
        //public bool IsFailure { get; }

        //public bool IsSuccess => !IsFailure;
        public ResultType Status { get; }
        public string? Error { get; }
        private readonly T _value;
        public T Value => Status == ResultType.ExecutedSuccessfully ? _value : throw new Exception(Error);

        private Result(string? error, T value, ResultType status)
        {
            _value = value;
            Status = status;
            Error = error;
        }

        public static Result<T> Success(T value)
        {
            return new Result<T>(default, value, ResultType.ExecutedSuccessfully);
        }

        public static Result<T> Failure(string message)
        {
            return new Result<T>(message, default, ResultType.Failed);
        }

        public static Result<T> QueuedForLaterExecution()
        {
            return new Result<T>(default, default, ResultType.QueuedForLaterExecution);
        }
    }

    public class Result : IResult
    {
        //public bool IsFailure { get; }

        //public bool IsSuccess => !IsFailure;
        public ResultType Status { get; }
        public string? Error { get; }

        private Result(string? error, ResultType status)
        {
            Error = error;
            Status = status;
        }

        public static Result Success()
        {
            return new Result(default, ResultType.ExecutedSuccessfully);
        }

        public static Result Failure(string error)
        {
            return new Result(error, ResultType.Failed);
        }

        public static Result QueuedForLaterExecution()
        {
            return new Result(default, ResultType.QueuedForLaterExecution);
        }
    }
}