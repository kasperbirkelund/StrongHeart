using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

#pragma warning disable 1591
namespace StrongHeart.EfCore.DesignTimeServices.Scaffold.Roslyn;

/// <summary>
/// Builds on Roslyn. Swap the namespace of a class.
/// </summary>
internal class AdjustNamespaceRewriter : CSharpSyntaxRewriter
{
    private readonly string _fullNewNameSpace;
    private AdjustNamespaceRewriter(string rootNameSpace, string subNameSpace)
    {
        string suffix =  subNameSpace == null ? string.Empty : $".{subNameSpace}";
        _fullNewNameSpace = $"{rootNameSpace}{suffix}";
    }

    public static string ApplyToCode(string fileContentAsCSharp, string rootNameSpace, string subNameSpace)
    {
        CSharpSyntaxTree tree = (CSharpSyntaxTree)CSharpSyntaxTree.ParseText(fileContentAsCSharp);
        var root = (CompilationUnitSyntax)tree.GetRoot();
        var rewriter = new AdjustNamespaceRewriter(rootNameSpace, subNameSpace);
        SyntaxNode newSource = rewriter.Visit(root);
        return newSource.ToString();
    }

    public override SyntaxNode? VisitNamespaceDeclaration(NamespaceDeclarationSyntax node)
    {
        return node
            .WithName(SyntaxFactory.ParseName(_fullNewNameSpace));
    }
}
#pragma warning restore 1591