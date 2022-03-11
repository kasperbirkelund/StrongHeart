using System.Xml.Serialization;

namespace StrongHeart.Build.Metrics
{
    public class Metric
    {
        [XmlAttribute("Name")] public string? Name { get; set; }
        [XmlAttribute("Value")] public string? Value { get; set; }
    }
}
