using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using StrongHeart.Features.Core;
using StrongHeart.Features.Documentation;
using StrongHeart.Features.Documentation.Sections;

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
            //DOC-START This is the only line
            return Task.FromResult(Result<TestQueryResponse>.Success(new TestQueryResponse(new PersonDto("PersonA"))));
            //DOC-END
        }

        public IEnumerable<ISection> GetDocumentationSections(DocumentationGenerationContext context)
        {
            yield return new TextSection("Rules");
            yield return Rules.MapToTableSection(x => new RuleCodes(x.Key, x.Value.GetType().Name, "Any description"));
            yield return new CodeCommentSection(GetType());
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