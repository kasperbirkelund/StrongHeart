using System;
using StrongHeart.Features.Decorators.Authorization;

namespace StrongHeart.Features.Core
{
    public interface IQueryFeature<in TRequest, TResponse> : IFeature<TRequest, Result<TResponse>>,
        IAuthorizable
        where TResponse : class, IResponseDto
        where TRequest : IRequest
    {
    }

    public interface IResult
    {
        bool IsFailure { get; }

        bool IsSuccess { get; }
        string? Error { get; }
    }

    public class Result : IResult
    {
        public bool IsFailure { get; }

        public bool IsSuccess => !IsFailure;
        public string? Error { get; }

        private Result(bool isFailure, string? error)
        {
            IsFailure = isFailure;
            Error = error;
        }

        public static Result Success()
        {
            return new Result(false, default);
        }
        public static Result Failure(string error)
        {
            return new Result(true, error);
        }
    }

    public class Result<T> : IResult
    {
        public bool IsFailure { get; }

        public bool IsSuccess => !IsFailure;
        public string? Error { get; }
        private readonly T _value;
        public T Value => IsSuccess ? _value : throw new Exception(Error);

        private Result(bool isFailure, string? error, T value)
        {
            _value = value;
            IsFailure = isFailure;
            Error = error;
        }

        public static Result<T> Success(T value)
        {
            return new Result<T>(false, default, value);
        }

        public static Result<T> Failure(string message)
        {
            return new Result<T>(true, message, default);
        }
    }
}