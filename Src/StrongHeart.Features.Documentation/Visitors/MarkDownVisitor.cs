using System.Linq;
using System.Reflection;
using System.Text;
using StrongHeart.Features.Documentation.Sections;

namespace StrongHeart.Features.Documentation.Visitors
{
    public class MarkDownVisitor : ISectionVisitor
    {
        private readonly StringBuilder _sb = new();

        public string AsString()
        {
            return _sb.ToString().TrimEnd();
        }

        public void VisitText(TextSection section)
        {
            if (section.IsHeader)
            {
                _sb.AppendLine($"### {section.Text}");
            }
            else
            {
                _sb.AppendLine(section.Text);
            }
        }

        public void VisitCodeComment(CodeCommentSection section)
        {
            foreach (CodeSnippet snippet in section.Snippets)
            {
                _sb.AppendLine(snippet.Title);
                _sb.AppendLine("```");
                _sb.AppendLine(snippet.Code.TrimEnd());
                _sb.AppendLine("```");
            }
        }

        public void VisitTable<T>(TableSection<T> section)
        {
            PropertyInfo[] properties = typeof(T).GetProperties();

            AddTableHeader(properties);
            AddTableBody(section, properties);
        }

        private void AddTableBody<T>(TableSection<T> section, PropertyInfo[] properties)
        {
            foreach (TableRow<T> row in section.Items)
            {
                _sb.Append("|");
                foreach (PropertyInfo t in properties)
                {
                    string? text = t.GetValue(row.Item)?.ToString();
                    _sb.Append($"{text}|");
                }
                _sb.AppendLine();
            }
        }

        private void AddTableHeader(PropertyInfo[] properties)
        {
            _sb.Append("|");
            foreach (PropertyInfo t in properties)
            {
                _sb.Append($"{t.GetPropertyName()}|");
            }
            _sb.AppendLine();
            string markDownRowDelimiter = "|" + string.Join(string.Empty, properties.Select(_ => "-|"));
            _sb.AppendLine(markDownRowDelimiter);
        }
    }
}