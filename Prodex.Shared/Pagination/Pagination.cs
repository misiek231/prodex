namespace Prodex.Shared.Pagination;

public class Pagination<T>
{
    public IEnumerable<T> Items { get; set; }

    public int TotalRows { get; set; }

    public Pagination(IEnumerable<T> items, int totalRows)
    {
        Items = items;
        TotalRows = totalRows;
    }
}
