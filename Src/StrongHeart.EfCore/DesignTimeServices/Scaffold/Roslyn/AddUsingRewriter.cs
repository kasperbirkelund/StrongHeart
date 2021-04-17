using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

#pragma warning disable 1591
namespace StrongHeart.EfCore.DesignTimeServices.Scaffold.Roslyn
{
    /// <summary>
    /// Builds on Roslyn. Add a using statement
    /// </summary>
    internal class AddUsingRewriter
    {
        public static string ApplyToCode(string fileContentAsCSharp, string usingToAdd)
        {
            CSharpSyntaxTree tree = (CSharpSyntaxTree)CSharpSyntaxTree.ParseText(fileContentAsCSharp);
            var root = (CompilationUnitSyntax)tree.GetRoot();
            root = root.AddUsings(SyntaxFactory.UsingDirective(SyntaxFactory.ParseName(" " + usingToAdd)));

            return root.ToString();
        }
    }
}
#pragma warning restore 1591