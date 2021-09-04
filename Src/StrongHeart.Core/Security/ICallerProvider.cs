namespace StrongHeart.Core.Security
{
    public interface ICallerProvider
    {
        ICaller GetCurrentCaller();
    }
}