namespace StrongHeart.Features.Core
{
    public interface IGetSingleItemResponse<out T> : IResponseDto
    {
        T Item { get; } //TODO: this should be nullable
    }
}