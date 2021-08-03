namespace StrongHeart.Features.Documentation.Sections
{
    public class TableRow<T>
    {
        public TableRow(T item)
        {
            Item = item;
        }

        public T Item { get; }
    }
}