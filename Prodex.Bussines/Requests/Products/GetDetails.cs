using MediatR;
using Prodex.Shared.Models.Products;

namespace Prodex.Bussines.Requests.Products;

public class GetDetails : IRequest<DetailsModel>
{
    public long Id { get; set; }

    public GetDetails(long id)
    {
        Id = id;
    }
}
