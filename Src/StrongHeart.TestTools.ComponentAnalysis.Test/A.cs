using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using StrongHeart.TestTools.ComponentAnalysis.Core.ReferenceChecker;

namespace StrongHeart.TestTools.ComponentAnalysis.Test
{
    public static class Components
    {
        public static IEnumerable<Component> GetAll()
        {
            var methods = typeof(Components).GetMethods(BindingFlags.Public | BindingFlags.Static);
            foreach (MethodInfo method in methods.Where(x => x.ReturnType == typeof(Component)))
            {
                var component = method.Invoke(null, null) as Component;
                yield return component;
            }
        }

        public static Component GetCommunication()
        {
            return new Component("Communication",
                new ProjectReferenceItem("CodeImprover.Cmd.Communication"),
                new ProjectReferenceItem("CodeImprover.Handler.EmailSender")
            );
        }

        public static Component GetMaint()
        {
            return new Component("Maintenance",
                new ProjectReferenceItem("CodeImprover.Cmd.Maintenance"),
                new ProjectReferenceItem("CodeImprover.Handler.Maintenance"),
                new ProjectReferenceItem("CodeImprover.Handler.Maintenance.Test")
            );
        }

        public static Component GetAnalyzer()
        {
            return new Component("Analyzer",
                new ProjectReferenceItem("CodeImprover.Cmd.SourceCodeAnalysis"),
                new ProjectReferenceItem("CodeImprover.DuplicationDetection"),
                new ProjectReferenceItem("CodeImprover.DuplicationDetection.Analysis"),
                new ProjectReferenceItem("CodeImprover.DuplicationDetection.Indexing"),
                new ProjectReferenceItem("CodeImprover.DuplicationDetection.Indexing.Test"),
                new ProjectReferenceItem("CodeImprover.DuplicationDetection.Search"),
                new ProjectReferenceItem("CodeImprover.DuplicationDetection.Search.Test"),
                new ProjectReferenceItem("CodeImprover.DuplicationDetection.Test"),
                new ProjectReferenceItem("CodeImprover.Extensions.DotNet"),
                new ProjectReferenceItem("CodeImprover.Extensions.DotNet.Test"),
                new ProjectReferenceItem("CodeImprover.Extensions.Analyzer.Abstractions"),
                new ProjectReferenceItem("CodeImprover.Extensions.Analyzer.Abstractions.Test"),
                new ProjectReferenceItem("CodeImprover.Extensions.Git"),
                new ProjectReferenceItem("CodeImprover.Extensions.Git.Tests"),
                new ProjectReferenceItem("CodeImprover.Extensions.SourceControl.Abstractions"),
                new ProjectReferenceItem("CodeImprover.Extensions.SourceControlExtension"),
                new ProjectReferenceItem("CodeImprover.Handler.PopulateReadModel"),
                new ProjectReferenceItem("CodeImprover.Handler.RunAnalysis"),
                new ProjectReferenceItem("CodeImprover.Handler.RunAnalysis.Test")
            );
        }

        public static Component GetWebApp()
        {
            return new Component("WebApp",
                new ProjectReferenceItem("CodeImprover.WebApi.Client"),
                new ProjectReferenceItem("CodeImprover.WebApp"),
                new ProjectReferenceItem("CodeImprover.WebApp.Business")
            );
        }

        public static Component GetWebApi()
        {
            return new Component("WebApi",
                new ProjectReferenceItem("CodeImprover.Api.Business"),
                new ProjectReferenceItem("CodeImprover.Api.Business.Test"),
                new ProjectReferenceItem("CodeImprover.DatabaseMigrations"),
                new ProjectReferenceItem("CodeImprover.DatabaseMigrations.Runner"),
                new ProjectReferenceItem("CodeImprover.DatabaseMigrations.Test"),
                new ProjectReferenceItem("CodeImprover.WebApi"),
                new ProjectReferenceItem("CodeImprover.HealthCheck")
            );
        }

        public static Component GetShared()
        {
            return new Component("Shared",
                new ProjectReferenceItem("CodeImprover.Core"),
                new ProjectReferenceItem("CodeImprover.Core.Test"),
                new ProjectReferenceItem("CodeImprover.DataRepository"),
                new ProjectReferenceItem("CodeImprover.Extensions.AzureDevOps"),
                new ProjectReferenceItem("CodeImprover.Extensions.Github"),
                new ProjectReferenceItem("CodeImprover.Extensions.Github.Test"),
                new ProjectReferenceItem("CodeImprover.Extensions.Publisher.MassTransit"),
                new ProjectReferenceItem("CodeImprover.Extensions.Subscriber.MassTransit")
            );
        }
    }

}
