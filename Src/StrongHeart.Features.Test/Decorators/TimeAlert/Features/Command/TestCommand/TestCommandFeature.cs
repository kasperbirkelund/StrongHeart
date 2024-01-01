using System;
using System.Threading.Tasks;
using StrongHeart.Features.Core;
using StrongHeart.Features.Decorators.TimeAlert;

namespace StrongHeart.Features.Test.Decorators.TimeAlert.Features.Command.TestCommand;

public class TestCommandFeature : ICommandFeature<TestCommandRequest, TestCommandDto>, ITimeAlert
{
    public async Task<Result> Execute(TestCommandRequest request)
    {
        await Task.Delay(request.Model.TimeToExecuteOnSeconds * 1000);
        return Result.Success();
    }

    public TimeSpan MaxAllowedExecutionTime => TimeSpan.FromMilliseconds(200);
}