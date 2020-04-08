﻿using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Scaffolding;
using Microsoft.EntityFrameworkCore.Scaffolding.Internal;
using Microsoft.EntityFrameworkCore.Storage.Internal;

namespace StrongHeart.EfCore.DesignTimeServices.Scaffold
{
    public class AddGeneratedClassToFileNameReverseEngineerScaffolder : ReverseEngineerScaffolder
    {
        public AddGeneratedClassToFileNameReverseEngineerScaffolder(IDatabaseModelFactory databaseModelFactory, IScaffoldingModelFactory scaffoldingModelFactory, IModelCodeGeneratorSelector modelCodeGeneratorSelector, ICSharpUtilities cSharpUtilities, ICSharpHelper cSharpHelper, INamedConnectionStringResolver connectionStringResolver) : base(databaseModelFactory, scaffoldingModelFactory, modelCodeGeneratorSelector, cSharpUtilities, cSharpHelper, connectionStringResolver)
        {
        }

        public override SavedModelFiles Save(ScaffoldedModel scaffoldedModel, string outputDir, bool overwriteFiles)
        {
            void toGeneratedFilePath(ScaffoldedFile old)
            {
                //make all generated files have .g.cs as suffix to indicate to SIG (and others) that this file is autogenerated.
                old.Path = old.Path.Replace(".cs", ".generated.cs");
                old.Code = AddAutoGeneratedClassMarkerRewriter.ApplyToCode(old.Code);
            }

            toGeneratedFilePath(scaffoldedModel.ContextFile);
            foreach (ScaffoldedFile additionalFile in scaffoldedModel.AdditionalFiles)
            {
                toGeneratedFilePath(additionalFile);
            }

            return base.Save(scaffoldedModel, outputDir, overwriteFiles);
        }
    }
}
