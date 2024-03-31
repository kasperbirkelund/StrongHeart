using System.Collections.Generic;
using System.Threading.Tasks;
using StrongHeart.Core.Security;
using StrongHeart.Features.Core;
using StrongHeart.Features.Documentation;
using StrongHeart.Features.Documentation.Sections;

namespace StrongHeart.DemoApp.Business.Features.Queries.GetCar;

public class GetCarFeature : QueryFeatureBase<GetCarRequest, GetCarResponse>
{
    private readonly IFoo _foo;

    public GetCarFeature(IFoo foo)
    {
        _foo = foo;
    }

    public override Task<Result<GetCarResponse>> Execute(GetCarRequest request)
    {
        _foo.DoWork(request.Caller);
        CarDetails? item = null;
        if (request.Id > 0)
        {
            item = new("Renault", 2012, "whatever", "whatever");
        }
        return Task.FromResult(Result<GetCarResponse>.Success(new GetCarResponse(item)));
    }

    protected override IEnumerable<ISection> OnGetDocumentationSections(DocumentationGenerationContext context)
    {
        yield break;
    }
}

public record CarDetails(string Brand, int Age, string Whatever, string Whatever2)
{
}

public class GetCarResponse : IGetSingleItemResponse<CarDetails>
{
    public GetCarResponse(CarDetails? item)
    {
        Item = item;
    }

    public CarDetails? Item { get; }
}

public class GetCarRequest : IRequest
{
    public GetCarRequest(int id, ICaller caller)
    {
        Caller = caller;
        Id = id;
    }

    public ICaller Caller { get; }
    public int Id { get; }
}