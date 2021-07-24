using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using Microsoft.CodeAnalysis;
using StrongHeart.DemoApp.Business.SourceCodeGenerator.Dto;

namespace StrongHeart.DemoApp.Business.SourceCodeGenerator.Helpers.Xml
{
    internal class XmlQueryFeatureReader : IFeatureReader<QueryFeatures>
    {
        public QueryFeatures GetFeatures(IEnumerable<AdditionalText> additionalFiles)
        {
            string xml = additionalFiles.GetFileContent(x => x.Path.EndsWith("queries.xml"));
            XmlSerializer serializer = new(typeof(QueryFeatures));

            using (TextReader reader = new StringReader(xml))
            {
                QueryFeatures result = (QueryFeatures)serializer.Deserialize(reader);
                return result;
            }
        }
    }
}