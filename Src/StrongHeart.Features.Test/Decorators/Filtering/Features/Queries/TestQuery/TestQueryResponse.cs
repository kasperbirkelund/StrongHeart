using System.Collections.Generic;
using StrongHeart.Features.Core;

namespace StrongHeart.Features.Test.Decorators.Filtering.Features.Queries.TestQuery;

public class TestQueryResponse : IGetListResponse<PersonDto>
{
    public TestQueryResponse(ICollection<PersonDto> items)
    {
        Items = items;
    }

    public ICollection<PersonDto> Items { get; }
}