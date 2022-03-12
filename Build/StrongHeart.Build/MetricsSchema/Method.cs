using System.Xml.Serialization;

namespace StrongHeart.Build.MetricsSchema;

public class Method
{
    [XmlAttribute("Name")]public string? Name { get; set; }
    public Metric[]? Metrics { get; set; }
}