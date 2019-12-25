using System.Collections.Generic;
using StrongHeart.Features.Test.SampleDecorator.SimpleLog;

namespace StrongHeart.Features.Test.Helpers
{
    public class SimpleLogSpy : ISimpleLog
    {
        public readonly List<string> Messages = new List<string>();

        public void Log(string message)
        {
            Messages.Add(message);
        }
    }
}