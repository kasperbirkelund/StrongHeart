namespace StrongHeart.Features.Documentation.Sections
{
    public class CodeSnippet
    {
        public string Title { get; }
        public string Code { get; }

        public CodeSnippet(string title, string code)
        {
            Title = title;
            Code = code;
        }
    }
}