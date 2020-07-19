using System.Collections.Generic;
using System.Linq;

namespace StrongHeart.Features.Documentation
{
    public class TableSection<T> : ISection
    {
        public IList<TableRow<T>> Items { get; }

        public TableSection(IEnumerable<TableRow<T>> items)
        {
            Items = items.ToArray();
        }

        public void Accept(ISectionVisitor visitor)
        {
            visitor.VisitTable(this);
        }
    }
}