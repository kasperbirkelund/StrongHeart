using System.Collections.Generic;

namespace StrongHeart.DemoApp.Business.SourceCodeGenerator
{
    public class QueryFeatures
    {
        public string RootNamespace { get; set; }
        public QueryFeature[] Items { get; set; }
    }

    public class CommandFeatures
    {
        public string RootNamespace { get; set; }
        public CommandFeature[] Items { get; set; }
    }

    public class QueryFeature
    {
        public string Name { get; set; }
        public QueryRequest Request { get; set; }
        public QueryResponse Response { get; set; }
    }

    public class CommandFeature
    {
        public string Name { get; set; }
        public CommandRequest Request { get; set; }
    }

    public class QueryRequest
    {
        public List<string> Properties { get; set; }
    }

    public class CommandRequest
    {
        public List<string> AdditionalRequestProperties { get; set; }
        public List<string> DtoProperties { get; set; }
    }

    public class QueryResponse
    {
        public bool IsListResponse { get; set; }
        public string ResponseTypeName { get; set; }
        public List<string> Properties { get; set; }

    }
}
