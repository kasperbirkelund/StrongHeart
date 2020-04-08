using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using StrongHeart.Core.Security;
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
        public AuditOptions AuditOptions => new AuditOptions(Guid.Parse("1bbea587-1a0e-498d-9475-0e7912d5a9bc"), "TestCommand", false);
        public Func<TestCommandRequest, IEnumerable<Guid?>> CorrelationKeySelector => request => new List<Guid?>();
        public IEnumerable<IRole> GetRequiredRoles()
        {
            yield break;
        }
    }
}