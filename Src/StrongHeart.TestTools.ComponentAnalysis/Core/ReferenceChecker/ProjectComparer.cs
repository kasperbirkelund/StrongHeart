using System.Collections.Generic;

namespace StrongHeart.TestTools.ComponentAnalysis.Core.ReferenceChecker;

public class ProjectComparer : IEqualityComparer<Project>
{
    public bool Equals(Project x, Project y)
    {
        if (ReferenceEquals(x, y)) return true;
        if (ReferenceEquals(x, null)) return false;
        if (ReferenceEquals(y, null)) return false;
        if (x.GetType() != y.GetType()) return false;
        return x.Name == y.Name;
    }

    public int GetHashCode(Project obj)
    {
        return obj.Name != null ? obj.Name.GetHashCode() : 0;
    }
}