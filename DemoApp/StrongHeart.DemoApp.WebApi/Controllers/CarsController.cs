using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using StrongHeart.DemoApp.Business.Features.Commands.CreateCar;
using StrongHeart.DemoApp.Business.Features.Queries.GetCar;
using StrongHeart.DemoApp.Business.Features.Queries.GetCars;
using StrongHeart.DemoApp.WebApi.Services;
using StrongHeart.Features.Core;
using Car = StrongHeart.DemoApp.Business.Features.Queries.GetCars.Car;
using CarDetailed = StrongHeart.DemoApp.Business.Features.Queries.GetCar.Car;

namespace StrongHeart.DemoApp.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CarsController : ApiBase
    {
        public CarsController(IClaimsProvider claimsProvider) : base(claimsProvider)
        {
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CarDetailed>> GetCar([FromServices] IQueryFeature<GetCarRequest, GetCarResponse> feature, int id)
        {
            GetCarRequest request = new(GetCaller());
            Result<GetCarResponse> result = await feature.Execute(request);
            return FromResultQuery(result, x => x.Item);
        }

        [HttpGet("{model}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ICollection<Car>>> GetCars([FromServices] IQueryFeature<GetCarsRequest, GetCarsResponse> feature, string model)
        {
            GetCarsRequest request = new(model, GetCaller());
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