using System;
using System.Threading.Tasks;
using StrongHeart.Features.Core;

namespace StrongHeart.DemoApp.Business.Tests;

public interface ITestExecutor : IDisposable
{
    Task<Result<TResponse>> RunQuery<TRequest, TResponse>(TRequest request)
        where TRequest : IRequest
        where TResponse : class, IResponseDto;

    Task<Result> RunCommand<TRequest, TRequestDto>(TRequest request)
        where TRequestDto : IRequestDto
        where TRequest : IRequest<TRequestDto>;
}