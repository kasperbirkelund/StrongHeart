using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.CodeAnalysis;

namespace StrongHeart.DemoApp.Business.SourceCodeGenerator.Helpers
{
    public static class Extensions
    {
        public static string GetFileContent(this IEnumerable<AdditionalText> additionalFiles, Predicate<AdditionalText> predicate)
        {
            string? content = additionalFiles
                .Where(x => predicate(x))
                .Select(x => x.GetText()?.ToString())
                .SingleOrDefault();

            if (content == null)
            {
                throw new FileNotFoundException("Cannot find file");
            }

            return content;
        }
    }
}