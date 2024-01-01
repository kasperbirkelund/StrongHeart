#pragma warning disable 1591
using System;

namespace StrongHeart.EfCore.DesignTimeServices.Scaffold.Roslyn;

internal class MakeClassInternalRewriter
{
    private MakeClassInternalRewriter()
    {
    }

    public static string ApplyToCode(string fileContentAsCSharp)
    {
        //simple but easy :-)
        return fileContentAsCSharp
            .Replace("public class", "internal class", StringComparison.CurrentCulture)
            .Replace("public partial class", "internal partial class", StringComparison.CurrentCulture);
    }
}
#pragma warning restore 1591