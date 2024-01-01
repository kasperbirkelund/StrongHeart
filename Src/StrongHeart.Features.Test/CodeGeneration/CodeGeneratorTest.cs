using StrongHeart.Features.CodeGeneration;
using Xunit;

namespace StrongHeart.Features.Test.CodeGeneration
{
    public class CodeGeneratorTest
    {
        [Fact]
        public void GenerateCode()
        {
            var result = CodeGenerator.GenerateCode(new AddCommandFeatureStrategy(), new AddFeatureSettings("TestProject", "MyFeature", "CommandFeatureBase"));
        }
    }
}
