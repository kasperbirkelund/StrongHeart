---
name: strongheart-commands
description: Implements StrongHeart command features (CQRS write side). Use when adding or modifying commands in an application using StrongHeart.Features, or when the user asks how to implement commands in StrongHeart.
---

# StrongHeart Commands

## Overview

Commands are write-side operations. Each command has a **Request** (with optional **Dto**), a **Feature** class that executes it, and optional **ValidateRequest** for input validation. Commands return `Result` (no response body) or use `ICompletedAction<T>` for side-channel results.

## Project setup

1. Create a **project-specific base class** that extends your `FeatureBase` and implements `ICommandFeature<TRequest, TRequestDto>` and `IRequestValidator<TRequest>`. Apply project-specific decorators via this base.
2. All command features in the project must inherit this base.

```csharp
public abstract class CommandFeatureBase<TRequest, TRequestDto> : FeatureBase, ICommandFeature<TRequest, TRequestDto>, IRequestValidator<TRequest>
    where TRequest : IRequest<TRequestDto>
    where TRequestDto : IRequestDto
{
    public abstract Task<Result> Execute(TRequest request);
    public abstract IEnumerable<ValidationMessage> ValidateRequest(TRequest request);
}
```

## Implementing a command

### 1. Request DTO (optional but typical)

- Implement `IRequestDto`.
- Holds the data needed for the command (e.g. ids, names).

```csharp
public class CreateCarDto : IRequestDto
{
    public CreateCarDto(string brand) => Brand = brand;
    public string Brand { get; }
}
```

### 2. Request

- Implement `IRequest<TRequestDto>` when the command has a body (Dto).
- Must have `ICaller Caller` and, if using a body, `TRequestDto Model`.
- Optionally implement `ICompletedAction<T>` to return a side-channel result (e.g. "number of items deleted") via a callback.

```csharp
public class CreateCarRequest : IRequest<CreateCarDto>
{
    public CreateCarRequest(Guid id, CreateCarDto model, ICaller caller)
    {
        Id = id;
        Caller = caller;
        Model = model;
    }
    public Guid Id { get; }
    public ICaller Caller { get; }
    public CreateCarDto Model { get; }
}
```

With completed action (e.g. delete):

```csharp
public class DeleteCarRequest : IRequest<DeleteCarDto>, ICompletedAction<DeleteCarCompletedAction>
{
    public DeleteCarRequest(DeleteCarDto model, ICaller caller, Action<DeleteCarCompletedAction> completedAction)
    {
        Caller = caller;
        Model = model;
        CompletedAction = completedAction;
    }
    public ICaller Caller { get; }
    public DeleteCarDto Model { get; }
    public Action<DeleteCarCompletedAction> CompletedAction { get; }
}
```

### 3. Feature class

- Inherit `CommandFeatureBase<TRequest, TRequestDto>`.
- Implement `Execute(TRequest request)` and `ValidateRequest(TRequest request)`.
- Inject dependencies via constructor.
- Return `Result.Success()`, `Result.ClientError(message)`, `Result.ServerError(message)`, or `Result.QueuedForLaterExecution()`.

```csharp
public class CreateCarFeature : CommandFeatureBase<CreateCarRequest, CreateCarDto>
{
    private readonly IEventPublisher _eventPublisher;

    public CreateCarFeature(IEventPublisher eventPublisher) => _eventPublisher = eventPublisher;

    public override async Task<Result> Execute(CreateCarRequest request)
    {
        await _eventPublisher.Publish(new CarCreatedEvent(request.Id));
        return Result.Success();
    }

    public override IEnumerable<ValidationMessage> ValidateRequest(CreateCarRequest request)
    {
        if (request.Model.Brand != "Skoda")
            yield return "Model must be Skoda";
    }
}
```

Using completed action inside Execute:

```csharp
request.CompletedAction(new DeleteCarCompletedAction(numberOfCarsDeleted));
return Task.FromResult(Result.Success());
```

### 4. Documentation (optional)

- Override `OnGetDocumentationSections(DocumentationGenerationContext context)` to add sections (e.g. `new CodeCommentSection(GetType())`, `new TextSection("...")`).

## Result types

- `Result.Success()` – command executed successfully.
- `Result.QueuedForLaterExecution()` – accepted, will run later (e.g. async/queue).
- `Result.ClientError(string error)` – validation/business rule failure.
- `Result.ServerError(string error)` – server/technical failure.

## Checklist

- [ ] Request DTO implements `IRequestDto` (if used).
- [ ] Request implements `IRequest<TRequestDto>` (and optionally `ICompletedAction<T>`).
- [ ] Request has `ICaller Caller`; if body, has `TRequestDto Model`.
- [ ] Feature inherits project `CommandFeatureBase<TRequest, TRequestDto>`.
- [ ] Feature implements `Execute` (returns `Task<Result>`) and `ValidateRequest` (yields `ValidationMessage`).
- [ ] Use `Result.Success()` / `QueuedForLaterExecution()` / `ClientError` / `ServerError` as appropriate.
