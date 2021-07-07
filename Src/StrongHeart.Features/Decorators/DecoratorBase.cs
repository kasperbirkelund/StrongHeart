using System;
using System.Threading.Tasks;
using StrongHeart.Features.Core;

namespace StrongHeart.Features.Decorators
{
    /// <summary>
    /// Base class for decorator which must share functionality across feature types (commands and queries)
    /// </summary>
    public abstract class DecoratorBase
    {
        protected abstract Task<TOut> Invoke<TRequest, TOut>(Func<TRequest, Task<TOut>> func, TRequest request)
            where TRequest : IRequest;
    }
}