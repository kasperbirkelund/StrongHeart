using System;
using System.IO;
using System.Reflection;
using System.Text;
using System.Web;
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

        public void VisitCodeComment(CodeCommentSection section)
        {
            foreach (CodeSnippet snippet in section.Snippets)
            {
                
                _sb.AppendLine($"<p>{Encode(snippet.Title)}</p>");
                _sb.AppendLine($"<code>{Encode(snippet.Code.TrimEnd())}</code>");
            }
        }

        public void VisitText(TextSection section)
        {
            if (section.IsHeader)
            {
                _sb.AppendLine($"<h2>{Encode(section.Text)}</h2>");
            }
            else
            {
                _sb.AppendLine($"<p>{Encode(section.Text)}</p>");
            }
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
                    _sb.AppendLine($"<td>{Encode(text)}</td>");
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
                _sb.AppendLine($"<th scope=\"col\">{Encode(t.GetPropertyName())}</th>");
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

        private string? Encode(string? input)
        {
            return input == null ? null : HttpUtility.HtmlEncode(input);
        }
    }
}