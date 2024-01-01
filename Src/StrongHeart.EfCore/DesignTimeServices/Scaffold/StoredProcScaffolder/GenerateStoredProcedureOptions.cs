using System.Collections.Generic;
using System.Linq;
using StrongHeart.EfCore.DesignTimeServices.Scaffold.CustomGenerators;

namespace StrongHeart.EfCore.DesignTimeServices.Scaffold.StoredProcScaffolder;

public class GenerateStoredProcedureOptions
{
    public GenerateStoredProcedureOptions(ICollection<string> explicitSchemasToInclude, ApplicationSpecificOptions applicationSpecificOptions)
    {
        ExplicitSchemasToInclude = explicitSchemasToInclude;
        ApplicationSpecificOptions = applicationSpecificOptions;
    }

    public ICollection<string> ExplicitSchemasToInclude { get; }
    public ApplicationSpecificOptions ApplicationSpecificOptions { get; }

    public string ToDebugString()
    {
        string schemas = "no restrictions";
        if (ExplicitSchemasToInclude.Any())
        {
            schemas = string.Join(", ", ExplicitSchemasToInclude);
        }

        return @$"{ApplicationSpecificOptions.ToDebugString()}
Explicit schemas to include: {schemas}";
    }
}