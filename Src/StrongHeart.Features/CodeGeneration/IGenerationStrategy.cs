namespace StrongHeart.Features.CodeGeneration;

interface IGenerationStrategy
{
    string Generate(AddFeatureSettings settings);
}