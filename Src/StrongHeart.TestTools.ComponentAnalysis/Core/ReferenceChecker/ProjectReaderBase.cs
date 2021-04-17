using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace StrongHeart.TestTools.ComponentAnalysis.Core.ReferenceChecker
{
    public abstract class ProjectReaderBase : IProjectReader
    {
        private readonly string _solutionRootDir;

        protected ProjectReaderBase(string solutionRootDir)
        {
            _solutionRootDir = solutionRootDir;
        }

        public IEnumerable<Project> GetProjectHierarchy()
        {
            var allSingleLevel = ReadProjects().ToArray();

            foreach (Project project in allSingleLevel)
            {
                var dep = GetAllDependencies(project, allSingleLevel)
                    .Distinct(new ProjectComparer())
                    .ToArray();

                yield return new Project(project.Name, dep);
            }
        }

        private IEnumerable<Project> GetAllDependencies(Project project, ICollection<Project> all)
        {
            yield return project;
            var refs = all.Single(x => x.Name == project.Name);
            foreach (var p2 in refs.References)
            {
                foreach (Project dependency in GetAllDependencies(p2, all))
                {
                    yield return dependency;
                }
            }
        }

        private IEnumerable<Project> ReadProjects()
        {
            var files = Directory.EnumerateFiles(_solutionRootDir, "*.csproj", SearchOption.AllDirectories);
            foreach (string file in files)
            {
                yield return ReadProject(file);
            }
        }

        protected abstract Project ReadProject(string file);
    }
}
