using System;
using System.IO;

namespace StrongHeart.FeatureTool;

class Program
{
    static void Main(string[] args)
    {
        string project = args[0];
        string type = args[1];
        string name = args[2];
        bool isList = false;
        if (args.Length > 3)
        {
            isList = Convert.ToBoolean(args[3]);
        }
        string dir = Path.Combine(Environment.CurrentDirectory, project);
        dir = Path.Combine(dir, "Features");
        dir = Path.Combine(dir, type);
        dir = Path.Combine(dir, name);

        Directory.CreateDirectory(dir);

        string file = name + "Feature.cs";
        string fullPath = Path.Combine(dir, file);

        string content = type switch
        {
            "Commands" => GetCommand(project, name),
            "Queries" => GetQuery(project, name, isList),
            _ => throw new ArgumentException("Unknown type. Use 'Commands' or 'Queries'")
        };

        File.WriteAllText(fullPath, content);
        Console.WriteLine("Done: " + fullPath);
    }


    private static string GetCommand(string project, string commandName)
    {
        string s =
            $@"using System.Threading.Tasks;
using StrongHeart.Core.Security;
using StrongHeart.Features.Core;

namespace {project}.Features.Commands.{commandName}
{{
    public record {commandName}Dto() : IRequestDto;
    public record {commandName}Request(ICaller Caller, {commandName}Dto Model) : IRequest<{commandName}Dto>;
    
    public partial class {commandName}Feature : ICommandFeature<{commandName}Request, {commandName}Dto>
    {{
        public Task<Result> Execute({commandName}Request request)
        {{
            throw new System.NotImplementedException();
        }}
    }}
}}";

        return s;
    }

    private static string GetQuery(string project, string queryName, bool isList)
    {
        string s = $@"using System.Threading.Tasks;
using System.Collections.Generic;
using StrongHeart.Core.Security;
using StrongHeart.Features.Core;

namespace {project}.Features.Queries.{queryName}
{{
    public record {queryName}Request(ICaller Caller) : IRequest;
    public record {queryName}();
    {GetResponseClass(queryName, isList)}

    public partial class {queryName}Feature : IQueryFeature<{queryName}Request, {queryName}Response>
    {{
        public Task<Result<{queryName}Response>> Execute({queryName}Request request)
        {{
            {GetResponseContent(queryName, isList)}
            {queryName}Response response = new(item{(isList ? "s" : string.Empty)});
            Result<{queryName}Response> result = Result<{queryName}Response>.Success(response);
            return Task.FromResult(result);
        }}
    }}
}}";
        return s;
    }

    private static string GetResponseContent(string queryName, bool isList)
    {
        if (isList)
        {
            return $"List<{queryName}> items = new();";
        }
        else
        {
            return $"{queryName} item = new();";
        }
    }

    private static string GetResponseClass(string queryName, bool isList)
    {
        if (isList)
        {
            return @$"public partial class {queryName}Response : IGetListResponse<{queryName}>
    {{
        public {queryName}Response(ICollection<{queryName}> items)
        {{
            Items = items;
        }}


        public ICollection<{queryName}> Items {{ get; }}
    }}";
        }
        else
        {
            return $@"public class {queryName}Response : IGetSingleItemResponse<{queryName}> 
    {{
        public {queryName}Response({queryName}? item)
        {{
            Item = item;
        }}

        public {queryName}? Item {{ get; }}
    }}";
        }
    }
}