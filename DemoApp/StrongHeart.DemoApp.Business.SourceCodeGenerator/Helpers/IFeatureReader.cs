using System.Collections.Generic;
using Microsoft.CodeAnalysis;

namespace StrongHeart.DemoApp.Business.SourceCodeGenerator.Helpers
{
    public interface IFeatureReader<out T>
    {
        T GetFeatures(IEnumerable<AdditionalText> additionalFiles);
    }
}
