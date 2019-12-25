namespace StrongHeart.Features.Core
{
    public interface IUser : ICaller
    {
        string Name { get; }
    }
}