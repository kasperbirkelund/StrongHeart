using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace StrongHeart.Features.Documentation
{
    public static class Extensions
    {
        public static void Accept(this ISectionVisitor visitor, IEnumerable<ISection> sections)
        {
            foreach (ISection section in sections)
            {
                section.Accept(visitor);
            }
        }

        public static TableSection<TDest> MapToTableSection<TSource, TDest>(this IEnumerable<TSource> source, Func<TSource, TDest> selector)
        {
            return new TableSection<TDest>(source.Select(x => new TableRow<TDest>(selector(x))));
        }

        public static string GetPropertyName(this MemberInfo property)
        {
            DescriptionAttribute? attribute = property.GetCustomAttribute<DescriptionAttribute>();
            return attribute == null ? property.Name : attribute.Description;
        }
    }
}