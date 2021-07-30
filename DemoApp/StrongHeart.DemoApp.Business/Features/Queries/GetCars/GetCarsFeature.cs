using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StrongHeart.Core.Security;
using StrongHeart.Features.Core;
using StrongHeart.Features.Decorators.Filtering;
using StrongHeart.Features.Decorators.Retry;
using StrongHeart.Features.Documentation.Sections;

namespace StrongHeart.DemoApp.Business.Features.Queries.GetCars
{
    public partial class GetCarsFeature : IFilterable<GetCarsResponse>, IRetryable
    {
        public override Task<Result<GetCarsResponse>> Execute(GetCarsRequest request)
        {
            List<Car> items = new()
            {
                new Car("Toyota", 2016),
                new Car("Fiat", 2018)
            };
            items = items.Where(x => request.Model == null || x.Model == request.Model).ToList();
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

        protected override IEnumerable<ISection> GetLocalDocumentationSections(ICaller caller)
        {
            yield break;
        }
    }
}
