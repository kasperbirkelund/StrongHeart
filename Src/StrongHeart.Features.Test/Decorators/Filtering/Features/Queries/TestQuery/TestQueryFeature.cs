using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using StrongHeart.Features.Core;
using StrongHeart.Features.Decorators.Filtering;

namespace StrongHeart.Features.Test.Decorators.Filtering.Features.Queries.TestQuery
{
    public class TestQueryFeature : IQueryFeature<TestQueryRequest, TestQueryResponse>, IFilterable<TestQueryResponse>
    {
        public IEnumerable<IRole> GetRequiredRoles() => throw new NotSupportedException();

        public Task<Result<TestQueryResponse>> Execute(TestQueryRequest request)
        {
            List<PersonDto> persons = new List<PersonDto>()
            {
                new PersonDto("PersonA"),
                new PersonDto("PersonB"),
                new PersonDto("PersonC"),
            };
            return Task.FromResult(Result.Success(new TestQueryResponse(persons)));
        }

        public TestQueryResponse GetFilteredItem(IFilterDecisionContext context, TestQueryResponse item)
        {
            List<PersonDto> personsFiltered = item.Items.Where(x => x.Name != "PersonA").ToList();
            return new TestQueryResponse(personsFiltered);
        }
    }
}