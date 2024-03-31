using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StrongHeart.Core.Security;
using StrongHeart.Features.Core;
using StrongHeart.Features.Decorators.Filtering;
using StrongHeart.Features.Decorators.Retry;
using StrongHeart.Features.Documentation;
using StrongHeart.Features.Documentation.Sections;

namespace StrongHeart.DemoApp.Business.Features.Queries.GetCars;

public class GetCarsFeature : QueryFeatureBase<GetCarsRequest, GetCarsResponse>, IFilterable<GetCarsResponse>, IRetryable
{
    public override Task<Result<GetCarsResponse>> Execute(GetCarsRequest request)
    {
        List<Car> items = new()
        {
            new Car("Toyota", 2016),
            new Car("Fiat", 2018)
        };
        items = items.Where(x => x.Brand == request.Brand).ToList();
        return Task.FromResult(Result<GetCarsResponse>.Success(new GetCarsResponse(items)));
    }

    public GetCarsResponse GetFilteredItem(IFilterDecisionContext context, GetCarsResponse response)
    {
        if (context.Caller.Claims.Any(x => x.Value == "dark-knight"))
        {
            return response with
            {
                Items = new List<Car>()
            };
        }
        return response;
    }

    public bool WhenExceptionIsThrownShouldIRetry(Exception exception, int currentAttempt)
    {
        //Some fancy algorithm...
        return false;
    }

    protected override IEnumerable<ISection> OnGetDocumentationSections(DocumentationGenerationContext context)
    {
        yield break;
    }
}

public record Car(string Brand, int Year);

public class GetCarsRequest : IRequest
{
    public GetCarsRequest(string brand, ICaller caller)
    {
        Caller = caller;
        Brand = brand;
    }

    public ICaller Caller { get; }
    public string Brand { get; }
}

public record GetCarsResponse(ICollection<Car> Items) : IGetListResponse<Car>;