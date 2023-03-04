using MediatR;
using Prodex.Shared.Pagination;
using Prodex.Shared.Utils;

namespace Prodex.Bussines.Requests.Processes
{
    public class ProcessesSelect : IRequest<Pagination<KeyValueResult>>
    {
        public Pager Pager { get; set; }
        public string Search { get; set; }

        public ProcessesSelect(Pager pager, string search)
        {
            Pager = pager;
            Search = search;
        }
    }
}
