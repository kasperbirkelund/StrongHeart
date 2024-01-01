using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace StrongHeart.DemoApp.Business.SourceCodeGenerator.Helpers;

public class EmbeddedResourceReader : IStringReader
{
    private readonly string _name;

    public EmbeddedResourceReader(string name)
    {
        _name = name;
    }

    public string[] ReadLines()
    {
        var text = ReadManifestData(_name);
        return text.Split(new[] { Environment.NewLine }, StringSplitOptions.None);

    }

    public static string ReadManifestData(string embeddedFileName)
    {
        Assembly assembly = typeof(EmbeddedResourceReader).Assembly;
        string resourceName = assembly.GetManifestResourceNames().Single(s => s.EndsWith(embeddedFileName, StringComparison.CurrentCultureIgnoreCase));

        using (StreamReader reader = new(assembly.GetManifestResourceStream(resourceName)!))
        {
            return reader.ReadToEnd();
        }
    }
}