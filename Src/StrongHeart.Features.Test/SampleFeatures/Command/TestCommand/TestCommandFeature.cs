using System.Collections.Generic;
using System.Threading.Tasks;
using StrongHeart.Core.Security;
using StrongHeart.Features.Core;
using StrongHeart.Features.Decorators.Authorization;

namespace StrongHeart.Features.Test.SampleFeatures.Command.TestCommand
{
    public class TestCommandFeature : ICommandFeature<TestCommandRequest, TestCommandDto>, IAuthorizable
    {
        public Task<Result> Execute(TestCommandRequest request)
        {
            return Task.FromResult(Result.Success());
        }
        public IEnumerable<IRole> GetRequiredRoles()
        {
            yield break;
        }
    }
}