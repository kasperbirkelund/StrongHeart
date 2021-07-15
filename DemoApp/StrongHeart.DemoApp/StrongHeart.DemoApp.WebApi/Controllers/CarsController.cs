using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using StrongHeart.DemoApp.Business.Features.Commands.CreateCars;
using StrongHeart.DemoApp.Business.Features.Queries.GetCars;
using StrongHeart.Features.Core;

namespace StrongHeart.DemoApp.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CarsController : ApiBase
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ICollection<Car>>> Get([FromServices] IQueryFeature<GetCarsRequest, GetCarsResponse> feature)
        {
            GetCarsRequest request = new(GetCaller());
            Result<GetCarsResponse> result = await feature.Execute(request);
            return FromResultQuery(result, x => x.Items);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post([FromServices] ICommandFeature<CreateCarsRequest, CreateCarsRequestDto> feature)
        {
            CreateCarsRequest request = new(new CreateCarsRequestDto(), GetCaller());
            Result result = await feature.Execute(request);
            return FromResultCommand(result, x => x.Ok());
        }
    }
}
