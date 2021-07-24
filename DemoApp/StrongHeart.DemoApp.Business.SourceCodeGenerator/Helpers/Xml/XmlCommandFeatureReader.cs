using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using Microsoft.CodeAnalysis;
using StrongHeart.DemoApp.Business.SourceCodeGenerator.Dto;

namespace StrongHeart.DemoApp.Business.SourceCodeGenerator.Helpers.Xml
{
    internal class XmlCommandFeatureReader : IFeatureReader<CommandFeatures>
    {
        public CommandFeatures GetFeatures(IEnumerable<AdditionalText> additionalFiles)
        {
            string xml = additionalFiles.GetFileContent(x=> x.Path.EndsWith("commands.xml"));
            XmlSerializer serializer = new(typeof(CommandFeatures));
            using (TextReader reader = new StringReader(xml))
            {
                CommandFeatures result = (CommandFeatures)serializer.Deserialize(reader);
                return result;
            }
        }
    }
}