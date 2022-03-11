using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using StrongHeart.Features.Core;

namespace StrongHeart.DemoApp.Business.Tests
{
    public class TestExecutorFactory
    {
        public static ITestExecutor Create(IServiceCollection services)
        {
            return new TestExecutor(services, true);
        }

        private class TestExecutor : ITestExecutor
        {
            private readonly bool _checkForSuccessExecutionResult;

            internal TestExecutor(IServiceCollection services, bool checkForSuccessExecutionResult)
            {
                _checkForSuccessExecutionResult = checkForSuccessExecutionResult;
                var provider = services.BuildServiceProvider();
                Scope = provider.CreateScope();
            }

            public IServiceScope Scope { get; }

            public void Dispose()
            {
                Scope.Dispose();
            }

            public async Task<Result<TResponse>> RunQuery<TRequest, TResponse>(TRequest request)
                where TRequest : IRequest
                where TResponse : class, IResponseDto
            {
                var feature = Scope.ServiceProvider.GetRequiredService<IQueryFeature<TRequest, TResponse>>();
                Result<TResponse> result = await feature.Execute(request);
                CheckStatus<TRequest>(result);

                return result;
            }

            public async Task<Result> RunCommand<TRequest, TRequestDto>(TRequest request)
                where TRequestDto : IRequestDto
                where TRequest : IRequest<TRequestDto>
            {
                var feature = Scope.ServiceProvider.GetRequiredService<ICommandFeature<TRequest, TRequestDto>>();
                Result result = await feature.Execute(request);
                CheckStatus<TRequest>(result);

                return result;
            }

            private void CheckStatus<TRequest>(IResult result)
            {
                if (_checkForSuccessExecutionResult && !result.IsSuccess)
                {
                    throw new InvalidOperationException(
                        $"Execution failed on request {typeof(TRequest).Name}: {result.Error}");
                }
            }
        }
    }
}