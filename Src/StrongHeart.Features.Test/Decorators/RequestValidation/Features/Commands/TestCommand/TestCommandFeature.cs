﻿using System.Threading.Tasks;
using FluentValidation;
using StrongHeart.Features.Core;
using StrongHeart.Features.Decorators.RequestValidation;

namespace StrongHeart.Features.Test.Decorators.RequestValidation.Features.Commands.TestCommand
{
    public class TestCommandFeature : ICommandFeature<TestCommandRequest, TestCommandDto>, IRequestValidatable
    {
        public Task<Result> Execute(TestCommandRequest request)
        {
            return Task.FromResult(Result.Success());
        }

        public IValidator GetValidator()
        {
            return new TestCommandRequestValidator();
        }
    }
}