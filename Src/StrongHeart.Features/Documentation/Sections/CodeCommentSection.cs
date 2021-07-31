using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using StrongHeart.Features.Documentation.Visitors;

namespace StrongHeart.Features.Documentation.Sections
{
    public class CodeCommentSection : ISection
    {
        private readonly Type _type;
        private readonly List<DocSnippet> _snippets = new();

        public CodeCommentSection(Type type)
        {
            _type = type;
        }

        public ICollection<DocSnippet> Snippets => _snippets;

        public void Accept(ISectionVisitor visitor)
        {
            DirectoryInfo dir = GetSourceCodeDir(_type);
            IEnumerable<FileInfo> files = dir.EnumerateFiles("*.cs", SearchOption.AllDirectories);
            foreach (var file in files.Where(x => !x.FullName.Contains(".generated.cs")))
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

        private IEnumerable<DocSnippet> GetSnippets(MethodDeclarationSyntax method)
        {
            string code = string.Empty;
            string title = string.Empty;
            bool isInScope = false;
            foreach (var line in method.Body.ToFullString()
                .Split(new[] {Environment.NewLine}, StringSplitOptions.RemoveEmptyEntries))
            {
                if (line.Contains("//DOC-START"))
                {
                    title = line.Replace("//DOC-START", string.Empty).Trim();
                    isInScope = true;
                }
                else if (line.Contains("//DOC-END"))
                {
                    isInScope = false;
                    yield return new DocSnippet(title, code);
                }
                else if (isInScope)
                {
                    code += line + Environment.NewLine;
                }
            }
        }

        private static DirectoryInfo GetSourceCodeDir(Type type)
        {
            string currentDir = Environment.CurrentDirectory;
            string searchString = @"\DemoApp\";
            int index = currentDir.IndexOf(searchString, StringComparison.Ordinal);
            string path = currentDir.Substring(0, index + searchString.Length);
            string pathFull = Path.Combine(path, type.Assembly.GetName().Name);
            return new DirectoryInfo(pathFull);
        }

        private static bool IsMatchingClass(ClassDeclarationSyntax arg, Type type)
        {
            var ns = arg.Parent as NamespaceDeclarationSyntax;
            return arg.Identifier.ValueText == type.Name && ns.Name.ToString() == type.Namespace;
        }
    }
}