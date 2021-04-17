﻿using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using StrongHeart.Core.Security;
using StrongHeart.Features.Core;
using StrongHeart.Features.Documentation;
using StrongHeart.Features.Test.Helpers;

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

        public IEnumerable<IRole> GetRequiredRoles()
        {
            yield return TestRole.Instance;
        }

        public IEnumerable<ISection> GetDocumentationSections(IUser user)
        {
            yield return new TextSection("Regler");

            var t = (Sum: 4.5, Count: 3);
            yield return Rules.MapToTableSection(x => new RuleCodes(x.Key, x.Value.GetType().Name));
            //yield return Rules.MapToTableSection(x => (DataType d, string s));
        }

        public class RuleCodes
        {
            public RuleCodes(DataType dataType, string rule)
            {
                Code = dataType.ToString();
                Rule = rule;
            }

            [Description("Kode")]
            public string Code { get; }
            [Description("Rule name")]
            public string Rule { get; }
        }
    }
}