namespace StrongHeart.Features.CodeGeneration;

internal class CodeGenerator
{
    public static GeneratedCode GenerateCode(IGenerationStrategy strategy, AddFeatureSettings settings)
    {
        string code = strategy.Generate(settings);
        Helper.GetRootDir()
    }
}