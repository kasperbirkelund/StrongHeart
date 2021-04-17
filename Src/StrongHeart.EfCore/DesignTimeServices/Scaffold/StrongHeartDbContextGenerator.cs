﻿using System;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Scaffolding;
using Microsoft.EntityFrameworkCore.Scaffolding.Internal;

namespace StrongHeart.EfCore.DesignTimeServices.Scaffold
{
    /// <summary>
    /// This class implements a hook into entity framework cores shaffolding engine which makes it possible to manipulate with the autogenerated DbContextFiles
    /// </summary>
    public class StrongHeartDbContextGenerator : CSharpDbContextGenerator
    {
        public StrongHeartDbContextGenerator(IProviderConfigurationCodeGenerator providerConfigurationCodeGenerator, IAnnotationCodeGenerator annotationCodeGenerator, ICSharpHelper cSharpHelper) : base(providerConfigurationCodeGenerator, annotationCodeGenerator, cSharpHelper)
        {
        }

        public override string WriteCode(IModel model, string contextName, string connectionString, string contextNamespace,
            string modelNamespace, bool useDataAnnotations, bool suppressConnectionStringWarning, bool suppressOnConfiguring)
        {
            string code = base.WriteCode(model, contextName, connectionString, contextNamespace, modelNamespace, useDataAnnotations, suppressConnectionStringWarning, suppressOnConfiguring);
            //string usingPrefix = "using Star.Foundation.Framework.DataAccess;" + Environment.NewLine;
            const string parameterlessConstructor = @"public MyDbContext()
        {
        }";

            string[] tablesToReplace = {
                "VersionInfo"
            };

            code = code
                .Replace(parameterlessConstructor, string.Empty);

            foreach (string s in tablesToReplace)
            {
                code = code.Replace($"public virtual DbSet<{s}> {s} {{ get; set; }}", $"//public virtual DbSet<{s}> {s} {{ get; set; }}");
            }

            return code;
        }

        protected override void GenerateOnConfiguring(string connectionString, bool suppressConnectionStringWarning)
        {
            //override default behaviour by doing nothing.
        }
    }
}