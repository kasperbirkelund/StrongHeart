using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace StrongHeart.TestTools.ComponentAnalysis.Core.ReferenceChecker
{
    public class DotNetCoreProjectReader : ProjectReaderBase
    {
        public DotNetCoreProjectReader(string solutionRootDir) : base(solutionRootDir)
        {
        }

        protected override Project ReadProject(string projectFile)
        {
            XDocument doc = XDocument.Parse(File.ReadAllText(projectFile));
            var v = doc
                .Root!
                .Descendants("ItemGroup")
                .Descendants("ProjectReference")
                .Select(x => x.Attribute("Include")!.Value)
                .Select(x => x.Split("\\").Last().Replace(".csproj", string.Empty))
                .Select(x => new Project(x));

            return new Project(Path.GetFileNameWithoutExtension(projectFile), v.ToArray());
        }
    }
}