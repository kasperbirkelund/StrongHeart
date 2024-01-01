using System.Collections.Generic;
using StrongHeart.Features.Core;

namespace StrongHeart.Features.Test.SampleFeatures.Queries.TestQuery;

public class TestQueryResponse : IGetListResponse<string>
{
    public TestQueryResponse(ICollection<string> items)
    {
        Items = items;
    }


    public ICollection<string> Items { get; }
}