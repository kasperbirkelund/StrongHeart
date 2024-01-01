namespace StrongHeart.Features.Core;

///// <summary>
///// Represents a synchronous command feature which can return a value.
///// </summary>
//public interface ISynchronousCommandFeature<in TRequest, TRequestDto, TResponse> : IFeature<TRequest, Result<TResponse>>
//    where TResponse : class, IResponseDto
//    where TRequest : IRequest<TRequestDto>
//    where TRequestDto : IRequestDto
//{
//}