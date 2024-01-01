using System.Collections.Generic;

namespace StrongHeart.DemoApp.Business.SourceCodeGenerator.Dto;

public class CommandRequest
{
    public List<string> AdditionalRequestProperties { get; set; }
    public List<string> DtoProperties { get; set; }
}