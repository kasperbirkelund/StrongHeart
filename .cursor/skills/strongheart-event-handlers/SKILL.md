---
name: strongheart-event-handlers
description: Implements StrongHeart event handlers that react to domain events and can invoke commands. Use when adding or modifying event handlers in an application using StrongHeart.Features and StrongHeart events, or when the user asks how to implement event handlers in StrongHeart.
---

# StrongHeart Event Handlers

## Overview

Event handlers subscribe to domain events (e.g. `CarCreatedEvent`). They receive `EventMessage<TEvent, TMetadata>` and can invoke commands (or other features) with a caller provided by `ICallerProvider`. Use a project-specific base that binds your metadata type.

## Project setup

Create an abstract base that fixes your metadata type (e.g. `DemoAppSpecificMetadata`) and implements `IEventHandler<TEvent, TMetadata>`:

```csharp
public abstract class EventHandlerBase<TEvent> : IEventHandler<TEvent, DemoAppSpecificMetadata>
    where TEvent : class, IEvent
{
    public abstract Task Execute(EventMessage<TEvent, DemoAppSpecificMetadata> evt);
}
```

## Implementing a handler

1. Inherit `EventHandlerBase<TEvent>` with your event type.
2. Inject `ICommandFeature<TRequest, TRequestDto>` (or other features) and `ICallerProvider`.
3. In `Execute`, build the request (using `_callerProvider.GetCurrentCaller()` for caller) and call the feature.

```csharp
public class CarCreatedHandler : EventHandlerBase<CarCreatedEvent>
{
    private readonly ICommandFeature<NewCarCustomerNotificationRequest, NewCarCustomerNotificationDto> _feature;
    private readonly ICallerProvider _callerProvider;

    public CarCreatedHandler(
        ICommandFeature<NewCarCustomerNotificationRequest, NewCarCustomerNotificationDto> feature,
        ICallerProvider callerProvider)
    {
        _feature = feature;
        _callerProvider = callerProvider;
    }

    public override async Task Execute(EventMessage<CarCreatedEvent, DemoAppSpecificMetadata> evt)
    {
        await _feature.Execute(new NewCarCustomerNotificationRequest(
            _callerProvider.GetCurrentCaller(),
            new NewCarCustomerNotificationDto()));
    }
}
```

## Checklist

- [ ] Handler inherits project `EventHandlerBase<TEvent>`.
- [ ] Implements `Execute(EventMessage<TEvent, TMetadata> evt)`.
- [ ] Uses `ICallerProvider.GetCurrentCaller()` when building requests for commands/queries.
- [ ] Registers handler with the StrongHeart event subscription mechanism used by the project.
