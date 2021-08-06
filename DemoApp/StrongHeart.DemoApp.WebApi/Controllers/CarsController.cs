using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StrongHeart.DemoApp.Business.Features.Commands.CreateCar;
using StrongHeart.DemoApp.Business.Features.Queries.GetCar;
using StrongHeart.DemoApp.Business.Features.Queries.GetCars;
using StrongHeart.DemoApp.WebApi.Services;
using StrongHeart.Features.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using StrongHeart.DemoApp.Business.Features.Commands.DeleteCar;
using StrongHeart.DemoApp.Business.Features.Commands.UpdateCar;
using StrongHeart.Features.Documentation;
using StrongHeart.Features.Documentation.Sections;

namespace StrongHeart.DemoApp.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    //DOC-START Make your controller inherit base class
    public class CarsController : ApiBase
    //DOC-END
        , IDocumentationDescriber
    {
        public CarsController(IClaimsProvider claimsProvider) : base(claimsProvider)
        {
        }

        //DOC-START HttpGet single object
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<CarDetails>> GetCar([FromServices] IQueryFeature<GetCarRequest, GetCarResponse> feature, int id)
        {
            GetCarRequest request = new(GetCaller());
            Result<GetCarResponse> result = await feature.Execute(request);
            return FromResultQuery(result, x => x.Item);
        }
        //DOC-END

        //DOC-START HttpGet multiple objects
        [HttpGet("{model}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ICollection<Car>>> GetCars([FromServices] IQueryFeature<GetCarsRequest, GetCarsResponse> feature, string model)
        {
            GetCarsRequest request = new(model, GetCaller());
            Result<GetCarsResponse> result = await feature.Execute(request);
            return FromResultQuery(result, x => x.Items);
        }
        //DOC-END

        //DOC-START HttpPost
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Guid>> Create([FromServices] ICommandFeature<CreateCarRequest, CreateCarDto> feature, [FromBody] CreateCarDto? dto)
        {
            Guid id = Guid.NewGuid();
            CreateCarRequest request = new(id, dto, GetCaller());
            Result result = await feature.Execute(request);
            return FromResultCommand(result, id);
        }
        //DOC-END

        //DOC-START HttpPut
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update([FromServices] ICommandFeature<UpdateCarRequest, UpdateCarDto> feature, [FromBody] UpdateCarDto? dto)
        {
            UpdateCarRequest request = new(dto, GetCaller());
            Result result = await feature.Execute(request);
            return FromResultCommand(result);
        }
        //DOC-END

        //DOC-START HttpDelete
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)] //In this API delete is done synchronously and will return 200
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete([FromServices] ICommandFeature<DeleteCarRequest, DeleteCarDto> feature, [FromRoute] Guid id)
        {
            DeleteCarRequest request = new(new DeleteCarDto(id), GetCaller());
            Result result = await feature.Execute(request);
            return FromResultCommand(result);
        }
        //DOC-END

        public string? DocName => DocumentationConstants.Setup;
        public int? Order => 3;

        //Explicit impl of interface to ensure that unit tests validates public api class correctly.
        IEnumerable<ISection> IDocumentationDescriber.GetDocumentationSections(DocumentationGenerationContext context)
        {
            yield return new CodeCommentSection(GetType());
        }
    }
}