using MediatR;
using Prodex.Shared.Models.ProductTemplates.Statuses;

namespace Prodex.Bussines.Requests.ProductTemplates.Statuses;

public class GetDetails : IRequest<FormModel>
{
    public long Id { get; set; }

    public GetDetails(long id)
    {
        Id = id;
    }
}
