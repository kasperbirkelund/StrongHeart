namespace StrongHeart.TestTools.ComponentAnalysis.Core.ReferenceChecker
{
    public record Project(string Name, params Project[] References);

    //public record ComponentItem(string Name) : IItem;
}