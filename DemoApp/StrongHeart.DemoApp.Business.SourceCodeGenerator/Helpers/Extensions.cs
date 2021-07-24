using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.CodeAnalysis;

namespace StrongHeart.DemoApp.Business.SourceCodeGenerator.Helpers
{
    public static class Extensions
    {
        public static string? GetFileContent(this IEnumerable<AdditionalText> additionalFiles, Predicate<AdditionalText> predicate)
        {
            return additionalFiles.GetFileContent(predicate, x => x.GetText()?.ToString());
        }

        public static T GetFileContent<T>(this IEnumerable<AdditionalText> additionalFiles, Predicate<AdditionalText> predicate, Func<AdditionalText, T> selector)
        {
            T? content = additionalFiles
                .Where(x => predicate(x))
                .Select(selector)
                .SingleOrDefault();

            if (content == null)
            {
                throw new FileNotFoundException("Cannot find file");
            }

            return content;
        }
    }
}