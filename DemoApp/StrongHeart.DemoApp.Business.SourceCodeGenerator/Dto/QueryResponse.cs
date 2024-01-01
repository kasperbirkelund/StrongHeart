using System.Collections.Generic;

namespace StrongHeart.DemoApp.Business.SourceCodeGenerator.Dto;

public class QueryResponse
{
    public bool IsListResponse { get; set; }
    public string ResponseTypeName { get; set; }
    public List<string> Properties { get; set; }

}