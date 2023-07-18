namespace StrongHeart.FeatureTool;

public class Helper
{
    public static string GetGeneratedCodeText()
    {
        return $"[System.CodeDom.Compiler.GeneratedCode(\"StrongHeart.FeatureTool\", $\"{typeof(Helper).Assembly.GetName().Version}\")]";
    }
}