﻿#pragma warning disable 1591
using System;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace StrongHeart.EfCore.DesignTimeServices.Scaffold.Roslyn;

/// <summary>
/// Builds on Roslyn. Applies a comment on the input class telling that the class is autogenerated and should not be edited
/// </summary>
internal class AddAutoGeneratedClassMarkerRewriter : CSharpSyntaxRewriter
{
    private readonly Type _generator;

    private AddAutoGeneratedClassMarkerRewriter(Type generator)
    {
        _generator = generator;
    }

    public static string ApplyToCode(string fileContentAsCSharp, Type generator)
    {
        CSharpSyntaxTree tree = (CSharpSyntaxTree)CSharpSyntaxTree.ParseText(fileContentAsCSharp);
        var root = (CompilationUnitSyntax)tree.GetRoot();
        var rewriter = new AddAutoGeneratedClassMarkerRewriter(generator);
        SyntaxNode newSource = rewriter.Visit(root);
        return newSource.ToString();
    }

    public override SyntaxNode? VisitClassDeclaration(ClassDeclarationSyntax node)
    {
        SyntaxTrivia whitespace = node.GetLeadingTrivia().LastOrDefault(x => x.Kind() == SyntaxKind.WhitespaceTrivia);

        ClassDeclarationSyntax newClassNode = node.WithLeadingTrivia(GetComment(whitespace, _generator));
        return newClassNode;
    }

    private static SyntaxTrivia GetComment(SyntaxTrivia whitespace, Type generator)
    {
        string doc =
            $@"{whitespace}/// <summary>
{whitespace}/// This code is autogenerated by StrongHeart framework and should not contain any handwritten code. 
{whitespace}/// Dive into this type to change the code generator: '{generator.FullName}'
{whitespace}/// </summary>
{whitespace}";
        return SyntaxFactory.Comment(doc);
    }
}
#pragma warning restore 1591