using System.Collections.Generic;

namespace StrongHeart.Features.Core
{
    public interface IGetListResponse<T> : IResponseDto
    {
        ICollection<T> Items { get; }
    }
}