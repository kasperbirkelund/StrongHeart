using System;
using System.Collections.Generic;
using System.Linq;

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
    }
}