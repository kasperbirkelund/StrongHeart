using System;

namespace StrongHeart.DemoApp.Business.SourceCodeGenerator.Helpers
{
    public class StringReader : IStringReader
    {
        private readonly string _s;

        public StringReader(string s)
        {
            _s = s;
        }

        public string[] ReadLines()
        {
            return _s.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
        }
    }
}