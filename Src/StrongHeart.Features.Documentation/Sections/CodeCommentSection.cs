using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using StrongHeart.Features.Core;
using StrongHeart.Features.Documentation.Visitors;

namespace StrongHeart.Features.Documentation.Sections
{
    public class CodeCommentSection : ISection
    {
        public static string? SourceCodeDir = null;
        private readonly Type _type;
        private readonly List<CodeSnippet> _snippets = new();

        public CodeCommentSection(Type type)
        {
            _type = type;
        }

        public ICollection<CodeSnippet> Snippets => _snippets;

        //HACK: This algorithm reads all source code every time the CodeCommentSection is called which is inefficient and improvable.
        //But as it only should run on demand is is not an urgent issue on smaller solutions.
        public void Accept(ISectionVisitor visitor)
        {
            if (string.IsNullOrWhiteSpace(SourceCodeDir) || !Directory.Exists(SourceCodeDir))
            {
                throw new DirectoryNotFoundException($"Remember to initialize {nameof(CodeCommentSection)}.{nameof(SourceCodeDir)} with the path to your source code");
            }

            DirectoryInfo dir = new(SourceCodeDir!);
            IEnumerable<FileInfo> files = dir.EnumerateFiles("*.cs", SearchOption.AllDirectories);
            foreach (var file in files.Where(x => !x.FullName.Contains(".generated.cs"))) //Consider making this filter configurable
            {
                SyntaxTree tree = CSharpSyntaxTree.ParseText(File.ReadAllText(file.FullName));
                CompilationUnitSyntax root = tree.GetCompilationUnitRoot();
                IEnumerable<MethodDeclarationSyntax> methods = root
                    .DescendantNodes()
                    .OfType<ClassDeclarationSyntax>()
                    .Where(x => IsMatchingClass(x, _type))
                    .SelectMany(x => x.DescendantNodes().OfType<MethodDeclarationSyntax>());

                foreach (MethodDeclarationSyntax method in methods)
                {
                    _snippets.AddRange(GetSnippets(method));
                }
            }
            visitor.VisitCodeComment(this);
        }

        private IEnumerable<CodeSnippet> GetSnippets(MethodDeclarationSyntax method)
        {
            string code = string.Empty;
            string title = string.Empty;
            bool isInSnippetScope = false;
            foreach (var line in method.Body.ToFullString().Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries))
            {
                if (line.Contains("//DOC-START"))
                {
                    title = line.Replace("//DOC-START", string.Empty).Trim();
                    isInSnippetScope = true;
                }
                else if (line.Contains("//DOC-END"))
                {
                    isInSnippetScope = false; //if DOC-END is not in place, no snippets will be returned.
                    yield return new CodeSnippet(title, code);
                }
                else if (isInSnippetScope)
                {
                    code += line + Environment.NewLine;
                }
            }
        }

        private static bool IsMatchingClass(ClassDeclarationSyntax arg, Type type)
        {
            var ns = arg.Parent as NamespaceDeclarationSyntax;
            return arg.Identifier.ValueText == type.Name && ns.Name.ToString() == type.Namespace;
        }

        public static string GetSourceCodeDir<TFeature>(string parentDirectory) where TFeature : IFeatureMarker
        {
            string currentDir = Environment.CurrentDirectory;
            int index = currentDir.IndexOf(parentDirectory, StringComparison.Ordinal);
            string path = currentDir.Substring(0, index + parentDirectory.Length);
            string pathFull = Path.Combine(path, typeof(TFeature).Assembly.GetName().Name);
            return pathFull;
        }
    }
}