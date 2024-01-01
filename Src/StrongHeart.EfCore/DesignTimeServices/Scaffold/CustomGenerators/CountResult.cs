using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.EntityFrameworkCore.Scaffolding;

namespace StrongHeart.EfCore.DesignTimeServices.Scaffold.CustomGenerators;

internal class CountResult
{
    private readonly int _numberOfLines;
    private readonly int _numberOfFiles;
    private static StringComparison culture = StringComparison.CurrentCulture;

    private CountResult(int numberOfLines, int numberOfFiles)
    {
        _numberOfLines = numberOfLines;
        _numberOfFiles = numberOfFiles;
    }

    public override string ToString()
    {
        return $@"{_numberOfFiles} files generated
{_numberOfLines} of code lines generated (not including comments, empty lines ect.)";
    }

    public static CountResult CountItems(IList<SavedModelFiles> files)
    {
        int numberOfLines = 0;
        int numberOfFiles = 0;

        foreach (var file in files)
        {
            numberOfLines += CountNumberOfLine(file.ContextFile);
            foreach (string? item in file.AdditionalFiles)
            {
                numberOfLines += CountNumberOfLine(item);
            }

            numberOfFiles += file.AdditionalFiles.Count + 1; //+1 is the context file    
        }

        return new CountResult(numberOfLines, numberOfFiles);
    }

    //https://stackoverflow.com/questions/5956092/how-to-count-the-number-of-code-lines-in-a-c-sharp-solution-without-comments-an
    private static int CountNumberOfLine(string fileName)
    {
        string codeLines = File.ReadAllText(fileName);
        int count = 0;
        int inComment = 0;
        StringReader sr = new StringReader(codeLines);
        {
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                if (IsRealCode(line.Trim(), ref inComment))
                {
                    count++;
                }
            }
        }
        return count;
    }

    private static bool IsRealCode(string trimmed, ref int inComment)
    {
        if (IsCommentMarker(trimmed, ref inComment))
        {
            return false;
        }

        return IsCode(trimmed, inComment);
    }

    private static bool IsCommentMarker(string trimmed, ref int inComment)
    {
        if (trimmed.StartsWith("//", culture))
        {
            return true;
        }

        if (trimmed.StartsWith("/*", culture) && trimmed.EndsWith("*/", culture))
        {
            return true;
        }

        if (trimmed.StartsWith("/*", culture))
        {
            inComment++;
            return true;
        }

        if (trimmed.EndsWith("*/", culture))
        {
            inComment--;
            return true;
        }

        return false;
    }

    private static bool IsCode(string trimmed, int inComment)
    {
        return inComment == 0 && (trimmed.Contains(";", culture) || StartWithKeyWord(trimmed));
    }

    private static bool StartWithKeyWord(string trimmed)
    {
        string[] keyWords = {"public", "private", "internal", "protected", "if", "using", "else" };
        return keyWords.Any(x => trimmed.StartsWith(x, culture));
    }
}