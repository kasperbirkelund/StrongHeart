using System;
using System.Globalization;

namespace StrongHeart.EfCore.DesignTimeServices.Scaffold.StoredProcScaffolder;

internal class StoredProcInputParameter
{
    public StoredProcInputParameter(string name, string sqlType, bool isNullable)
    {
        Name = Format(name);
        CSharpType = sqlType.GetCSharpType(isNullable);
    }

    //format @ThisIsAParameter > thisIsAParameter
    private string Format(string name)
    {
        string a = name.Replace("@", string.Empty, StringComparison.CurrentCulture);
        return char.ToLower(a[0], CultureInfo.CurrentCulture) + a.Substring(1);
    }

    public string Name { get; }
    public string CSharpType { get; }
}