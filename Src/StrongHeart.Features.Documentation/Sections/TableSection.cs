using System.Collections.Generic;
using System.Linq;
using StrongHeart.Features.Documentation.Visitors;

namespace StrongHeart.Features.Documentation.Sections;

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