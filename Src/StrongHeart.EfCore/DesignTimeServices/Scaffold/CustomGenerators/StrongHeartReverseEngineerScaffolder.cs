﻿#pragma warning disable 1591
#pragma warning disable EF1001
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Design.Internal;
using Microsoft.EntityFrameworkCore.Scaffolding;
using Microsoft.EntityFrameworkCore.Scaffolding.Internal;
using StrongHeart.EfCore.DesignTimeServices.Scaffold.Roslyn;
using StrongHeart.EfCore.DesignTimeServices.Scaffold.StoredProcScaffolder;

namespace StrongHeart.EfCore.DesignTimeServices.Scaffold.CustomGenerators;

public class StrongHeartReverseEngineerScaffolder : ReverseEngineerScaffolder
{
    public StrongHeartReverseEngineerScaffolder(IDatabaseModelFactory databaseModelFactory, IScaffoldingModelFactory scaffoldingModelFactory, IModelCodeGeneratorSelector modelCodeGeneratorSelector, ICSharpUtilities cSharpUtilities, ICSharpHelper cSharpHelper, IDesignTimeConnectionStringResolver connectionStringResolver, IOperationReporter reporter, ApplicationSpecificOptions applicationSpecificOptions) : base(databaseModelFactory, scaffoldingModelFactory, modelCodeGeneratorSelector, cSharpUtilities, cSharpHelper, connectionStringResolver, reporter)
    {
        ApplicationSpecificOptions = applicationSpecificOptions;
    }

    public ApplicationSpecificOptions ApplicationSpecificOptions { get; }
    private string? _rootNamespace = null;
    private string? _contextName = null;
        
    public override ScaffoldedModel ScaffoldModel(string connectionString, DatabaseModelFactoryOptions databaseOptions, ModelReverseEngineerOptions modelOptions, ModelCodeGenerationOptions codeOptions)
    {
        _rootNamespace = codeOptions.ModelNamespace;
        _contextName = codeOptions.ContextName;
        ScaffoldedModel contextAndEntities = base.ScaffoldModel(connectionString, databaseOptions, modelOptions, codeOptions);

        GenerateStoredProcedureOptions options = new GenerateStoredProcedureOptions(databaseOptions.Schemas.ToArray(), ApplicationSpecificOptions);
        foreach (ScaffoldedFile file in GetAutogeneratedStoredProcedures(codeOptions, options))
        {
            contextAndEntities.AdditionalFiles.Add(file);
        }
            
        return contextAndEntities;
    }

    public override SavedModelFiles Save(ScaffoldedModel scaffoldedModel, string outputDir, bool overwriteFiles)
    {
        void ToGeneratedFilePath(ScaffoldedFile old)
        {
            //make all generated files have .generated.cs as suffix to indicate to SIG and developers that this file is autogenerated.
            old.Path = old.Path.Replace(".cs", ".generated.cs", StringComparison.CurrentCulture);
            old.Code = AddAutoGeneratedClassMarkerRewriter.ApplyToCode(old.Code, GetType());
        }

        ToGeneratedFilePath(scaffoldedModel.ContextFile);
        foreach (ScaffoldedFile additionalFile in scaffoldedModel.AdditionalFiles)
        {
            ToGeneratedFilePath(additionalFile);
        }

        List<SavedModelFiles> list = GetScaffoldedModelParts(scaffoldedModel, outputDir)
            .Select(part => base.Save(part.scaffoldedModel, part.outputDir, overwriteFiles))
            .ToList();

        CountResult result = CountResult.CountItems(list);
        Console.Out.WriteLine(result);

        return Combine(list);
    }

    private static IEnumerable<ScaffoldedFile> GetAutogeneratedStoredProcedures(ModelCodeGenerationOptions codeOptions, GenerateStoredProcedureOptions options)
    {
        ICollection<StoredProcDefinitionModel> model = StoredProcDefinitionModel.GetStoredProcModel(codeOptions.ConnectionString, options).ToArray();

        return Scaffolder.GetStoredProcedureFiles(model, codeOptions.ModelNamespace, codeOptions.ContextName);
    }

    private SavedModelFiles Combine(IReadOnlyCollection<SavedModelFiles> list)
    {
        return new SavedModelFiles(list.First().ContextFile, list.SelectMany(x => x.AdditionalFiles));
    }

    private IEnumerable<(ScaffoldedModel scaffoldedModel, string outputDir)> GetScaffoldedModelParts(ScaffoldedModel scaffoldedModel, string outputDir)
    {
        PartHelper helper = new PartHelper(scaffoldedModel, outputDir, _rootNamespace, _contextName);

        yield return helper.GetWriteModelPart();
        yield return helper.GetStoredProcedurePart();
        yield return helper.GetReadModelPart();
        yield return helper.GetContextPart();
    }
}
#pragma warning restore 1591
#pragma warning restore EF1001