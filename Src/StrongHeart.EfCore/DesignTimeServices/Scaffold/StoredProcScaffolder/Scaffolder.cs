using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Formatting;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Formatting;
using Microsoft.CodeAnalysis.Options;
using Microsoft.EntityFrameworkCore.Scaffolding;

namespace StrongHeart.EfCore.DesignTimeServices.Scaffold.StoredProcScaffolder
{
    internal class Scaffolder
    {
        public static IEnumerable<ScaffoldedFile> GetStoredProcedureFiles(ICollection<StoredProcDefinitionModel> model, string rootNameSpace, string contextName)
        {
            return GenerateStoredProcedureFile(model, rootNameSpace, contextName)
                .Concat(GenerateReadModelFile(model, rootNameSpace))
                .Concat(ContextPartialFile(model, rootNameSpace, contextName));
        }

        private static IEnumerable<ScaffoldedFile> ContextPartialFile(IEnumerable<StoredProcDefinitionModel> model, string rootNameSpace, string contextName)
        {
            ClassDeclarationSyntax[] classes =
            {
                GetContextPartialClassCode(model, contextName)
            };
            return classes.Select(x => GenerateCodeFile(GetFilePath(ScaffoldFileNamePrefixConstants.DbContextPartial, x.Identifier.ValueText + ".partial"), x, rootNameSpace));
        }

        private static IEnumerable<ScaffoldedFile> GenerateReadModelFile(IEnumerable<StoredProcDefinitionModel> model, string rootNameSpace)
        {
            IEnumerable<ClassDeclarationSyntax> classes = model.Select(GetReadModelClassCode);
            return classes.Select(x => GenerateCodeFile(GetFilePath(ScaffoldFileNamePrefixConstants.ReadModels, x.Identifier.ValueText), x, rootNameSpace));
        }

        private static IEnumerable<ScaffoldedFile> GenerateStoredProcedureFile(IEnumerable<StoredProcDefinitionModel> model, string rootNameSpace, string contextName)
        {
            IEnumerable<ClassDeclarationSyntax> classes = model.Select(m => GetStoredProcedureClassCode(m, contextName));
            return classes.Select(x => GenerateCodeFile(GetFilePath(ScaffoldFileNamePrefixConstants.StoredProcedure, x.Identifier.ValueText), x, rootNameSpace));
        }

        private static string GetFilePath(in string prefix, in string className)
        {
            return $"{prefix}{className}.cs";
        }

        /// <summary>
        /// Wrapper to ensure that all files are generated similarly
        /// </summary>
        private static ScaffoldedFile GenerateCodeFile(string path, ClassDeclarationSyntax classes, string rootNameSpace)
        {
            NamespaceDeclarationSyntax ns = SyntaxFactory.NamespaceDeclaration(SyntaxFactory.ParseName(rootNameSpace));

            ns = ns.AddMembers(classes);
            CompilationUnitSyntax syntaxFactory = SyntaxFactory.CompilationUnit();
            syntaxFactory = syntaxFactory.AddUsings(GetUsings(rootNameSpace));
            syntaxFactory = syntaxFactory.AddMembers(ns);

            SyntaxNode formattedResult = FormatDocument(syntaxFactory);
            string code = formattedResult.ToFullString();

            ScaffoldedFile file = new ScaffoldedFile
            {
                Path = path,
                Code = code
            };

            return file;
        }

        /// <summary>
        /// This method configures how the output file is formatted.
        /// </summary>
        private static SyntaxNode FormatDocument(CompilationUnitSyntax syntaxFactory)
        {
            using Workspace workspace = new AdhocWorkspace();
            OptionSet options = workspace.Options;
            options = options.WithChangedOption(CSharpFormattingOptions.NewLinesForBracesInTypes, true);
            var formattedResult = Formatter.Format(syntaxFactory, workspace, options);
            return formattedResult;
        }

        /// <summary>
        /// Generate code to get ReadModels into the DbContext awareness
        /// </summary>
        private static ClassDeclarationSyntax GetContextPartialClassCode(IEnumerable<StoredProcDefinitionModel> procedures, string contextName)
        {
            string properties = string.Join(Environment.NewLine, procedures.Select(x => $"public virtual DbSet<{GetReadModelName(x.Name)}> {GetReadModelName(x.Name)} {{ get; set; }}"));
            string hasNoKeyStatements = string.Join(Environment.NewLine, procedures.Select(x => $"modelBuilder.Entity<{GetReadModelName(x.Name)}>().HasNoKey();"));
            SyntaxTree tree = SyntaxFactory.ParseSyntaxTree($@"partial class {contextName}
    {{
        {properties}

        //This method will ensure that the stored procedure read models don't NEED to have a primary key returned. Read the doc for details.
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder)
        {{
            {hasNoKeyStatements}
        }}
    }}");
            return tree.GetRoot().DescendantNodes().OfType<ClassDeclarationSyntax>().Single();
        }

        /// <summary>
        /// Generate code to generate the ReadModels
        /// </summary>
        private static ClassDeclarationSyntax GetReadModelClassCode(StoredProcDefinitionModel procedures)
        {
            string properties = string.Join(Environment.NewLine, procedures.Output.Select(x => $"public {x.CSharpType} {x.Name} {{ get; set; }}"));

            SyntaxTree tree = SyntaxFactory.ParseSyntaxTree($@"public class {GetReadModelName(procedures.Name)} : {nameof(IReadModel)}
    {{
        {properties}
    }}");
            return tree.GetRoot().DescendantNodes().OfType<ClassDeclarationSyntax>().Single();
        }

        /// <summary>
        /// Generate code to wrap each stored procedure into a class
        /// </summary>
        private static ClassDeclarationSyntax GetStoredProcedureClassCode(StoredProcDefinitionModel procedures, string contextName)
        {
            string constructorArguments = string.Join(", ", procedures.Input.Select(x => $"{x.CSharpType} {x.Name}"));
            string statements = string.Join(Environment.NewLine, procedures.Input.Select(x => $"            Parameters.Add({x.Name});")); ;

            SyntaxTree tree = SyntaxFactory.ParseSyntaxTree($@"public class {procedures.Name}Proc : StoredProcBase<{contextName}, {GetReadModelName(procedures.Name)}>
    {{
        public override string Name => ""{procedures.Schema}.{procedures.Name}"";

        public override Func<{contextName}, DbSet<{GetReadModelName(procedures.Name)}>> EntityFunc => context => context.{GetReadModelName(procedures.Name)};

        public {procedures.Name}Proc({constructorArguments})
        {{
{statements}
        }}
    }}");
            return tree.GetRoot().DescendantNodes().OfType<ClassDeclarationSyntax>().Single();
        }

        /// <summary>
        /// Generate needed using statements. Some might not be needed in all classes but that doesn't matter.
        /// </summary>
        private static UsingDirectiveSyntax[] GetUsings(string rootNameSpace)
        {
            string[] usings = {
                "Microsoft.EntityFrameworkCore",
                typeof(IReadModel).Namespace,
                rootNameSpace + ".ReadModels",
                rootNameSpace + ".WriteModels",
                rootNameSpace + ".Context",
                "System"
            };
            return usings.Select(x => SyntaxFactory.UsingDirective(SyntaxFactory.ParseName(" " + x))).ToArray(); //we must add a whitespace to ensure proper rendering. Roslyn might provide a better way...
        }

        private static string GetReadModelName(string procName)
        {
            return procName + "Model";
        }
    }
}