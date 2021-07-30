using System;
using StrongHeart.Core.Security;

namespace StrongHeart.DemoApp.Business.Features.Queries.GetCar
{
    public class Foo : IFoo
    {
        public void DoWork(ICaller caller)
        {
            Console.WriteLine("Whatever...");
        }
    }
}