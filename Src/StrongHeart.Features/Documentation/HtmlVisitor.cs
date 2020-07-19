using System.ComponentModel;
using System.Reflection;
using System.Text;

namespace StrongHeart.Features.Documentation
{
    public class HtmlVisitor : ISectionVisitor
    {
        public StringBuilder Sb { get; } = new StringBuilder();

        public void VisitText(TextSection section)
        {
            Sb.Append($"<p>{section.Text}</p>");
        }

        public void VisitTable<T>(TableSection<T> section)
        {
            Sb.Append("<table>");

            PropertyInfo[] properties = typeof(T).GetProperties();

            AddTableHeader(properties);
            AddTableBody(section, properties);

            Sb.Append("</table>");
        }

        private void AddTableBody<T>(TableSection<T> section, PropertyInfo[] properties)
        {
            Sb.Append("<tbody>");
            foreach (TableRow<T> row in section.Items)
            {
                Sb.Append("<tr>");
                foreach (PropertyInfo t in properties)
                {
                    string? text = t.GetValue(row.Item)?.ToString();
                    Sb.Append($"<td>{text}</td>");
                }

                Sb.Append("</tr>");
            }

            Sb.Append("</tbody>");
        }

        private void AddTableHeader(PropertyInfo[] properties)
        {
            Sb.Append("<thead><tr>");
            foreach (PropertyInfo t in properties)
            {
                Sb.Append($"<th scope=\"col\">{GetPropertyName(t)}</th>");
            }

            Sb.Append("</tr></thead>");
        }

        private static string GetPropertyName(MemberInfo property)
        {
            DescriptionAttribute? attribute = property.GetCustomAttribute<DescriptionAttribute>();
            return attribute == null ? property.Name : attribute.Description;
        }
    }
}