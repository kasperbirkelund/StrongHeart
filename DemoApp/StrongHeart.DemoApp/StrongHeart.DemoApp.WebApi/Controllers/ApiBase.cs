using System;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using StrongHeart.Core.Security;
using StrongHeart.Features.Core;

namespace StrongHeart.DemoApp.WebApi.Controllers
{
    public abstract class ApiBase : ControllerBase
    {
        protected IActionResult FromResultCommand(Result result, 
            HttpStatusCode serverErrorStatusCode = HttpStatusCode.InternalServerError,
            HttpStatusCode clientErrorStatusCode = HttpStatusCode.BadRequest)
        {
            return FromResultCommand(result, x => x.Ok(), serverErrorStatusCode, clientErrorStatusCode);
        }

        protected IActionResult FromResultCommand(Result result, Func<ControllerBase, IActionResult> executedSuccessfullySelector, 
            HttpStatusCode serverErrorStatusCode = HttpStatusCode.InternalServerError,
            HttpStatusCode clientErrorStatusCode = HttpStatusCode.BadRequest)
        {
            return result.Status switch
            {
                ResultType.ExecutedSuccessfully => executedSuccessfullySelector(this),
                ResultType.QueuedForLaterExecution => Accepted(),
                ResultType.ClientError => StatusCode((int)clientErrorStatusCode, result.Error),
                ResultType.ServerError => StatusCode((int)serverErrorStatusCode, "Internal server error"),
                _ => Ok()
            };
        }

        protected ActionResult<TValue> FromResultQuery<T, TValue>(Result<T> result, Func<T, TValue> selector, 
            HttpStatusCode serverErrorStatusCode = HttpStatusCode.InternalServerError,
            HttpStatusCode clientErrorStatusCode = HttpStatusCode.BadRequest)
        {
            return result.Status switch
            {
                ResultType.ExecutedSuccessfully => Ok(selector(result.Value)),
                ResultType.QueuedForLaterExecution => Accepted(),
                ResultType.ClientError => StatusCode((int)clientErrorStatusCode, result.Error),
                ResultType.ServerError => StatusCode((int)serverErrorStatusCode, "Internal server error"),
                _ => Ok()
            };
        }
        protected ICaller GetCaller()
        {
            return new WebApiCaller();
        }
    }
}