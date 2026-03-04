---
name: strongheart-queries
description: Implements StrongHeart query features (CQRS read side). Use when adding or modifying queries in an application using StrongHeart.Features, or when the user asks how to implement queries in StrongHeart.
---

# StrongHeart Queries

## Overview

Queries are read-side operations. Each query has a **Request**, a **Response** DTO, and a **Feature** class that returns `Result<TResponse>`. Responses use `IGetListResponse<T>` for lists or `IGetSingleItemResponse<T>` for single items (or any `IResponseDto`).

## Project setup

1. Create a **project-specific base class** that extends your `FeatureBase` and implements `IQueryFeature<TRequest, TResponse>`. Apply project-specific decorators via this base.
2. All query features in the project must inherit this base.

```csharp
public abstract class QueryFeatureBase<TRequest, TResponse> : FeatureBase, IQueryFeature<TRequest, TResponse>
    where TResponse : class, IResponseDto
    where TRequest : IRequest
{
    public abstract Task<Result<TResponse>> Execute(TRequest request);
}
```

## Implementing a query

### 1. Request

- Implement `IRequest` (no body) or `IRequest<TRequestDto>` (with body).
- Must have `ICaller Caller` and any query parameters as properties.

```csharp
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
```

```csharp
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
```

### 2. Response

- Implement `IResponseDto`.
- For lists: implement `IGetListResponse<T>` with `ICollection<T> Items`.
- For single item: implement `IGetSingleItemResponse<T>` with `T? Item`.

Single item:

```csharp
public class GetCarResponse : IGetSingleItemResponse<CarDetails>
{
    public GetCarResponse(CarDetails? item) => Item = item;
    public CarDetails? Item { get; }
}
```

List:

```csharp
public record GetCarsResponse(ICollection<Car> Items) : IGetListResponse<Car>;
```

### 3. Feature class

- Inherit `QueryFeatureBase<TRequest, TResponse>`.
- Implement `Execute(TRequest request)` returning `Task<Result<TResponse>>`.
- Optionally implement decorator interfaces: `IFilterable<TResponse>` (filter result by caller/context), `IRetryable` (retry policy).

```csharp
public class GetCarFeature : QueryFeatureBase<GetCarRequest, GetCarResponse>
{
    private readonly IFoo _foo;

    public GetCarFeature(IFoo foo) => _foo = foo;

    public override Task<Result<GetCarResponse>> Execute(GetCarRequest request)
    {
        _foo.DoWork(request.Caller);
        CarDetails? item = request.Id > 0 ? new("Renault", 2012, "a", "b") : null;
        return Task.FromResult(Result<GetCarResponse>.Success(new GetCarResponse(item)));
    }
}
```

List query with optional decorators:

```csharp
public class GetCarsFeature : QueryFeatureBase<GetCarsRequest, GetCarsResponse>, IFilterable<GetCarsResponse>, IRetryable
{
    public override Task<Result<GetCarsResponse>> Execute(GetCarsRequest request)
    {
        var items = new List<Car> { new("Toyota", 2016), new("Fiat", 2018) };
        items = items.Where(x => x.Brand == request.Brand).ToList();
        return Task.FromResult(Result<GetCarsResponse>.Success(new GetCarsResponse(items)));
    }

    public GetCarsResponse GetFilteredItem(IFilterDecisionContext context, GetCarsResponse response)
    {
        if (context.Caller.Claims.Any(x => x.Value == "dark-knight"))
            return response with { Items = new List<Car>() };
        return response;
    }

    public bool WhenExceptionIsThrownShouldIRetry(Exception exception, int currentAttempt) => false;
}
```

### 4. Documentation (optional)

- Override `OnGetDocumentationSections(DocumentationGenerationContext context)`; use `yield break` for no extra sections.

## Result

- `Result<T>.Success(response)` – query executed, return response.
- `Result<T>.ClientError(string error)` – invalid request or business rule.
- `Result<T>.ServerError(string error)` – server/technical failure.
- `Result<T>.QueuedForLaterExecution(value)` – for read models that are eventually consistent (rare).

## Optional decorator interfaces

- **IFilterable&lt;TResponse&gt;** – implement `GetFilteredItem(IFilterDecisionContext context, TResponse response)` to adjust the response (e.g. by caller claims).
- **IRetryable** – implement `WhenExceptionIsThrownShouldIRetry(Exception exception, int currentAttempt)` for custom retry logic.

## Checklist

- [ ] Request implements `IRequest` (and has `ICaller Caller` + query parameters).
- [ ] Response implements `IResponseDto`; use `IGetListResponse<T>` or `IGetSingleItemResponse<T>` where appropriate.
- [ ] Feature inherits project `QueryFeatureBase<TRequest, TResponse>`.
- [ ] Feature implements `Execute` returning `Task<Result<TResponse>>`.
- [ ] Return `Result<TResponse>.Success(response)` or `.ClientError` / `.ServerError` as appropriate.
- [ ] Add `IFilterable<TResponse>` / `IRetryable` only if the project uses those decorators.
