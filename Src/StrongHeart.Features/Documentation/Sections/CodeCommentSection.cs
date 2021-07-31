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
            DirectoryInfo dir = GetDir(_type);
            var files = dir.EnumerateFiles("*.cs", SearchOption.AllDirectories);
            foreach (var file in files.Where(x => !x.FullName.Contains(".generated.cs")))
            {
                SyntaxTree tree = CSharpSyntaxTree.ParseText(File.ReadAllText(file.FullName));
                CompilationUnitSyntax root = tree.GetCompilationUnitRoot();
                var c = root
                    .DescendantNodes()
                    .OfType<ClassDeclarationSyntax>()
                    .Where(x => MyPredicate(x, _type));

                foreach (ClassDeclarationSyntax cl in c)
                {
                    foreach (MethodDeclarationSyntax method in cl.DescendantNodes().OfType<MethodDeclarationSyntax>())
                    {
                        string code = string.Empty;
                        string title = string.Empty;
                        bool isInScope = false;
                        foreach (var line in method.Body.ToFullString().Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries))
                        {
                            if (line.Contains("//DOC-START"))
                            {
                                title = line.Replace("//DOC-START", string.Empty).Trim();
                                isInScope = true;
                            }
                            else if (line.Contains("//DOC-END"))
                            {
                                isInScope = false;
                                _snippets.Add(new DocSnippet(title, code));
                            }
                            else if (isInScope)
                            {
                                code += line + Environment.NewLine;
                            }

                        }
                    }
                }
            }
            visitor.VisitCodeComment(this);
        }

        private static DirectoryInfo GetDir(Type type)
        {
            string currentDir = Environment.CurrentDirectory;
            string searchString = @"\DemoApp\";
            int index = currentDir.IndexOf(searchString, StringComparison.Ordinal);
            string path = currentDir.Substring(0, index + searchString.Length);
            string pathFull = Path.Combine(path, type.Assembly.GetName().Name);
            return new DirectoryInfo(pathFull);
        }

        private static bool MyPredicate(ClassDeclarationSyntax arg, Type type)
        {
            var ns = arg.Parent as NamespaceDeclarationSyntax;
            return arg.Identifier.ValueText == type.Name && ns.Name.ToString() == type.Namespace;

        }
    }
}