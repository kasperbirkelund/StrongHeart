using StrongHeart.Core.Security;

namespace StrongHeart.Features.Core;

public interface IRequest
{
    ICaller Caller { get; }
}

public interface IRequest<out T> : IRequest
    where T : IRequestDto
{
    T Model { get; }
}