using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;

namespace StrongHeart.Features.Test.Decorators.RequestValidation.Features.Commands.TestCommand;

public class ValidationDependency : IValidationDependency
{
    public Task IsValidDate(DateTime value, ValidationContext<TestCommandRequest> context, CancellationToken arg3)
    {
        const int minYear = 2000;
        context.AddFailure($"Only year after {minYear} is allowed");
        return Task.FromResult(value.Year > minYear);
    }
}