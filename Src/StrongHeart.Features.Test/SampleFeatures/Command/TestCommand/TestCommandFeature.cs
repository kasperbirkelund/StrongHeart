using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using StrongHeart.Features.Core;
using StrongHeart.Features.Decorators.Audit;

namespace StrongHeart.Features.Test.SampleFeatures.Command.TestCommand
{
    public class TestCommandFeature : ICommandFeature<TestCommandRequest, TestCommandDto>
    {
        public Task<Result> Execute(TestCommandRequest request)
        {
            return Task.FromResult(Result.Success());
        }

        public Func<TestCommandRequest, bool> IsOnBehalfOfOtherSelector => request => false; 
        public AuditOptions AuditOptions => new AuditOptions(Guid.NewGuid(), "TestCommand", false);
        public Func<TestCommandRequest, IEnumerable<Guid?>> CorrelationKeySelector => request => new List<Guid?>();
        public IEnumerable<IRole> GetRequiredRoles()
        {
            yield break;
        }
    }
}