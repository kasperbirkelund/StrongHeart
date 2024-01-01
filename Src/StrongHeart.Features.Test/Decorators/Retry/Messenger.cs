namespace StrongHeart.Features.Test.Decorators.Retry;

public class Messenger
{
    public int Counter = 0;
    public void MethodIsExecuting()
    {
        Counter++;
    }
}