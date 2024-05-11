using System;

namespace StrongHeart.Features.Core;

public interface ICompletedAction<in T>
{
    Action<T> CompletedAction { get; }
}