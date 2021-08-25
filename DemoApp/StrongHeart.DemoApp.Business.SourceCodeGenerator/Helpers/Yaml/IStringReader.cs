using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.CodeAnalysis;

namespace StrongHeart.DemoApp.Business.SourceCodeGenerator.Helpers.Yaml
{
    public interface IStringReader
    {
        string[] ReadLines();
    }

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

    public class AdditionalTextStringReader : IStringReader
    {
        private readonly IEnumerable<AdditionalText> _additionalFiles;
        private readonly string _fileName;

        public AdditionalTextStringReader(IEnumerable<AdditionalText> additionalFiles, string fileName)
        {
            _additionalFiles = additionalFiles;
            _fileName = fileName;
        }

        public string[] ReadLines()
        {
            var path = _additionalFiles.GetFileContent(x => x.Path.EndsWith(_fileName), text => text.Path)!;
            return File.ReadAllLines(path);
        }
    }
}