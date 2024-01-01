using System;
using StrongHeart.Features.Core.Events;

namespace StrongHeart.DemoApp.Business.Events;

public record CarCreatedEvent(Guid Id) : IEvent;