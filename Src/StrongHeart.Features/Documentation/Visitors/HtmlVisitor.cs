using System.IO;
using System.Reflection;
using System.Text;
using System.Xml;
using StrongHeart.Features.Documentation.Sections;

namespace StrongHeart.Features.Documentation.Visitors
{
    public class HtmlVisitor : ISectionVisitor
    {
        private readonly StringBuilder _sb = new();

        public string AsString(bool includeLeadingHtmlTags)
        {
            if (includeLeadingHtmlTags)
            {
                string html = "<html>" + _sb + "</html>";
                string prettyHtml = PrettyXml(html);
                return prettyHtml;
            }
            return _sb.ToString();
        }

        public void VisitHeader(HeaderSection section)
        {
            _sb.AppendLine($"<h2>{section.Text}</h2>");
        }

        public void VisitText(TextSection section)
        {
            _sb.AppendLine($"<p>{section.Text}</p>");
        }

        public void VisitTable<T>(TableSection<T> section)
        {
            _sb.AppendLine("<table>");

            PropertyInfo[] properties = typeof(T).GetProperties();

            AddTableHeader(properties);
            AddTableBody(section, properties);

            _sb.AppendLine("</table>");
        }

        private void AddTableBody<T>(TableSection<T> section, PropertyInfo[] properties)
        {
            _sb.AppendLine("<tbody>");
            foreach (TableRow<T> row in section.Items)
            {
                _sb.AppendLine("<tr>");
                foreach (PropertyInfo t in properties)
                {
                    string? text = t.GetValue(row.Item)?.ToString();
                    _sb.AppendLine($"<td>{text}</td>");
                }

                _sb.AppendLine("</tr>");
            }

            _sb.AppendLine("</tbody>");
        }

        private void AddTableHeader(PropertyInfo[] properties)
        {
            _sb.AppendLine("<thead><tr>");
            foreach (PropertyInfo t in properties)
            {
                _sb.AppendLine($"<th scope=\"col\">{t.GetPropertyName()}</th>");
            }

            _sb.AppendLine("</tr></thead>");
        }

        private static string PrettyXml(string xml)
        {
            using (MemoryStream mStream = new())
            {
                using (XmlTextWriter writer = new(mStream, Encoding.Unicode))
                {
                    XmlDocument document = new();
                    document.LoadXml(xml);
                    writer.Formatting = Formatting.Indented;
                    document.WriteContentTo(writer);
                    writer.Flush();
                    mStream.Flush();
                    mStream.Position = 0;
                    using (StreamReader sReader = new(mStream))
                    {
                        return sReader.ReadToEnd();
                    }
                }
            }
        }
    }
}