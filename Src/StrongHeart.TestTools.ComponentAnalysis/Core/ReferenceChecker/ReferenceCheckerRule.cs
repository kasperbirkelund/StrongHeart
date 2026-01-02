using System;
using System.Collections.Generic;
using System.Linq;

namespace StrongHeart.TestTools.ComponentAnalysis.Core.ReferenceChecker;

public static class AEx
{
    extension(IEnumerable<Component> source)
    {
        public void MatchAllExistingProjects(IProjectReader projectReader)
    {
        throw new NotImplementedException();
    }

    public void DoesNotHaveRedundancy()
    {
        throw new NotImplementedException();
    }
    }

    public static ITargetComponentSelected DoesNotReference(this Component source, Component target)
    {
        return new TargetComponentSelected(source, target);
    }

    public static void Using(this ITargetComponentSelected component, IProjectReader projectReader)
    {
        Project[] result = projectReader.GetProjectHierarchy().ToArray();
        ReferenceCheckerEngine.B(component.Source, component.Target, result);
    }
}

public interface ITargetComponentSelected
{
    Component Source { get; }
    Component Target { get; }
}

internal class TargetComponentSelected : ITargetComponentSelected
{
    public Component Source { get; }
    public Component Target { get; }

    public TargetComponentSelected(Component source, Component target)
    {
        Source = source;
        Target = target;
    }
}