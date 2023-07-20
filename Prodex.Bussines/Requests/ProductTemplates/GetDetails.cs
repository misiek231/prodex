using MediatR;
using Prodex.Shared.Models.ProductTemplates;

namespace Prodex.Bussines.Requests.ProductTemplates;

public class GetDetails : IRequest<FormModel>
{
    public long Id { get; set; }

    public GetDetails(long id)
    {
        Id = id;
    }
}
