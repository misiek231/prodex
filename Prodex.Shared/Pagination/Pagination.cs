namespace Prodex.Shared.Pagination;

public class Pagination<T>
{
    public IEnumerable<T> Items { get; set; }

    public int TotalRows { get; set; }

    public Pagination(IEnumerable<T> items)
    {
        Items = items;
        TotalRows = Items.Count();
    }
}
