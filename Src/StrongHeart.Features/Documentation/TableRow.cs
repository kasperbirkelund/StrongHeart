namespace StrongHeart.Features.Documentation
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