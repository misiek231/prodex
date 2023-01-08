namespace Prodex.Shared.Pagination;

public class Pager
{

    public Pager(int pageIndex = 1, int pageSize = 20, string sort = "Id", string order = "ASC")
    {
        PageIndex = pageIndex;
        PageSize = pageSize;
        Sort = sort;
        Order = order;
    }

    public int PageIndex { get; set; }
    public int PageSize { get; set; }
    public string Sort { get; set; }
    public string Order { get; set; }
}
