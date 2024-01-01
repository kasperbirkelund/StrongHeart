namespace StrongHeart.Features.CodeGeneration
{
    internal class GeneratedCode
    {
        public string Content { get; }
        public string FileName { get; }

        public GeneratedCode(string content, string fileName)
        {
            Content = content;
            FileName = fileName;
        }
    }
}
