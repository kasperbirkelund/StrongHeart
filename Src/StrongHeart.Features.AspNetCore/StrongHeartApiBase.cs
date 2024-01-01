using System;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using StrongHeart.Features.Core;

namespace StrongHeart.Features.AspNetCore;

public abstract class StrongHeartApiBase : ControllerBase
{
    protected const HttpStatusCode
        DefaultServerErrorStatusCode = HttpStatusCode.InternalServerError,
        DefaultClientErrorStatusCode = HttpStatusCode.BadRequest;

    protected virtual ActionResult<T> FromResultCommand<T>(Result result, T args, HttpStatusCode serverErrorStatusCode = DefaultServerErrorStatusCode, HttpStatusCode clientErrorStatusCode = DefaultClientErrorStatusCode)
    {
        Result<T> genericResult = Result<T>.FromResult(result, args);
        return FromResultQuery(genericResult, x => x, serverErrorStatusCode, clientErrorStatusCode);
    }

    protected virtual IActionResult FromResultCommand(Result result, HttpStatusCode serverErrorStatusCode = DefaultServerErrorStatusCode, HttpStatusCode clientErrorStatusCode = DefaultClientErrorStatusCode)
    {
        return FromResultCommand(result, x => x.Ok(), serverErrorStatusCode, clientErrorStatusCode);
    }

    protected virtual IActionResult FromResultCommand(Result result, Func<ControllerBase, IActionResult> executedSuccessfullySelector, HttpStatusCode serverErrorStatusCode = DefaultServerErrorStatusCode, HttpStatusCode clientErrorStatusCode = DefaultClientErrorStatusCode)
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

    protected virtual ActionResult<TValue> FromResultQuery<T, TValue>(Result<T> result, Func<T, TValue?> selector, HttpStatusCode serverErrorStatusCode = DefaultServerErrorStatusCode, HttpStatusCode clientErrorStatusCode = DefaultClientErrorStatusCode)
    {
        return result.Status switch
        {
            ResultType.ExecutedSuccessfully => selector(result.Value) == null ? NotFound() : Ok(selector(result.Value)),
            ResultType.QueuedForLaterExecution => Accepted(selector(result.Value)),
            ResultType.ClientError => StatusCode((int)clientErrorStatusCode, result.Error),
            ResultType.ServerError => StatusCode((int)serverErrorStatusCode, "Internal server error"),
            _ => Ok(selector(result.Value))
        };
    }
}