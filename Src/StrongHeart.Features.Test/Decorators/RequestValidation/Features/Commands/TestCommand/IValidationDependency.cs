using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;

namespace StrongHeart.Features.Test.Decorators.RequestValidation.Features.Commands.TestCommand;

public interface IValidationDependency
{
    Task IsValidDate(DateTime arg1, ValidationContext<TestCommandRequest> arg2, CancellationToken arg3);
}