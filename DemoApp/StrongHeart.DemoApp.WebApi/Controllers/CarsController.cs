using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using StrongHeart.DemoApp.Business.Features.Commands.CreateCar;
using StrongHeart.DemoApp.Business.Features.Queries.GetCars;
using StrongHeart.DemoApp.WebApi.Services;
using StrongHeart.Features.Core;

namespace StrongHeart.DemoApp.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CarsController : ApiBase
    {
        public CarsController(IClaimsProvider claimsProvider) : base(claimsProvider)
        {
        }

        [HttpGet()]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ICollection<Car>>> Get([FromServices] IQueryFeature<GetCarsRequest, GetCarsResponse> feature, [FromQuery] int? year = null)
        {
            GetCarsRequest request = new(year, GetCaller());
            Result<GetCarsResponse> result = await feature.Execute(request);
            return FromResultQuery(result, x => x.Items);
        }

        [HttpPost()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post([FromServices] ICommandFeature<CreateCarRequest, CreateCarDto> feature, [FromBody] CreateCarDto? dto)
        {
            Guid id = Guid.NewGuid();
            CreateCarRequest request = new(id, dto, GetCaller());
            Result result = await feature.Execute(request);
            return FromResultCommand(result);
        }
    }
}