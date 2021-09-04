using Microsoft.AspNetCore.Mvc;
using StrongHeart.Core.FeatureToggling;
using StrongHeart.Core.Security;
using StrongHeart.DemoApp.WebApi.Services;
using StrongHeart.DemoApp.WebApi.Toggles;

namespace StrongHeart.DemoApp.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class FeatureToggleController : ApiBase
    {
        

        [HttpGet]
        public IActionResult GetToggledValue([FromServices] IFeatureToggle<MyToggle> toggle)
        {
            if (toggle.IsFeatureEnabled)
            {
                return Ok("Toggle is enabled");
            }

            return NotFound("Toggle is disabled");
        }

        public FeatureToggleController(ICallerProvider callerProvider) : base(callerProvider)
        {
        }
    }
}