using System.Text.Json.Serialization;

namespace Prodex.Shared.Pagination;

public class Pager
{
    private int pageIndex;

    public Pager(int pageIndex = 1, int pageSize = 20, string sort = "Id", string order = "ASC")
    {
        PageSize = pageSize;
        PageIndex = pageIndex;
        Sort = sort;
        Order = order;
    }

    public int PageSize { get; set; }
    public int? TotalPages => TotalRows.HasValue ? ((TotalRows - 1) / PageSize) + 1 : null;
    [JsonIgnore]
    public int? TotalRows { get; set; }
    public int PageIndex { 
        get => pageIndex; 
        set
        {
            pageIndex = value;
            if (pageIndex < 1) pageIndex = 1;
            if (TotalPages.HasValue && pageIndex > TotalPages) pageIndex = TotalPages.Value;
        }
    }
    public string Sort { get; set; }
    public string Order { get; set; }
}
