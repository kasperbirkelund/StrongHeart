using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using StrongHeart.Core.Security;
using StrongHeart.Features.Core;
using StrongHeart.Features.Documentation;

namespace StrongHeart.Features.Test.Documentation.Features.Queries.TestQuery
{
    public class TestQueryFeature : IQueryFeature<TestQueryRequest, TestQueryResponse>, IDocumentationDescriber
    {
        public IDictionary<DataType, IRule> Rules { get; } = new Dictionary<DataType, IRule>
        {
            {DataType.A, new RuleA()},
            {DataType.B, new RuleB()}
        };

        public Task<Result<TestQueryResponse>> Execute(TestQueryRequest request)
        {
            return Task.FromResult(Result<TestQueryResponse>.Success(new TestQueryResponse(new PersonDto("PersonA"))));
        }

        public IEnumerable<ISection> GetDocumentationSections(ICaller user)
        {
            yield return new TextSection("Rules");
            yield return Rules.MapToTableSection(x => new RuleCodes(x.Key, x.Value.GetType().Name, "Any description"));
        }

        private class RuleCodes
        {
            public RuleCodes(DataType dataType, string ruleName, string description)
            {
                Code = dataType.ToString();
                Rule = ruleName;
                Description = description;
            }

            [Description("Kode")]
            public string Code { get; }
            [Description("Rule name")]
            public string Rule { get; }
            [Description("Description")]
            public string Description { get; }
        }
    }
}