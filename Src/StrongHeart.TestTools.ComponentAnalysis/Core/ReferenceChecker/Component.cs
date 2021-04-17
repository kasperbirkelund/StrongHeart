namespace StrongHeart.TestTools.ComponentAnalysis.Core.ReferenceChecker
{
    public record Component(string Name, params IItem[] ChildItems) : IItem;
}