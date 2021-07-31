namespace StrongHeart.Features.Documentation.Sections
{
    public class DocSnippet
    {
        public string Title { get; }
        public string Code { get; }

        public DocSnippet(string title, string code)
        {
            Title = title;
            Code = code;
        }
    }
}