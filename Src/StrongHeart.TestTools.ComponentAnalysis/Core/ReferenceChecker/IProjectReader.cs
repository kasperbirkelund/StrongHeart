using System.Collections.Generic;

namespace StrongHeart.TestTools.ComponentAnalysis.Core.ReferenceChecker
{
    public interface IProjectReader
    {
        IEnumerable<Project> GetProjectHierarchy();
    }
}