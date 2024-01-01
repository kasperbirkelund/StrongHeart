#pragma warning disable 1591
#pragma warning disable EF1001

using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Scaffolding.Internal;

namespace StrongHeart.EfCore.DesignTimeServices.Scaffold.CustomGenerators;

/// <summary>
/// This class makes modifications to the generated entities (write models).
/// </summary>
public class StrongHeartCSharpEntityTypeGenerator : CSharpEntityTypeGenerator
{
    public StrongHeartCSharpEntityTypeGenerator(IAnnotationCodeGenerator annotationCodeGenerator, ICSharpHelper cSharpHelper) 
        : base(annotationCodeGenerator, cSharpHelper)
    {
    }
}

#pragma warning restore 1591
#pragma warning restore EF1001