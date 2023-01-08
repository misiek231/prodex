using MediatR;
using Prodex.Bussines.Requests;

namespace Prodex.Bussines.HandlersHelpers
{
    public abstract class BaseCreateHandler<TForm, TResponse> : IRequestHandler<CreateRequest<TForm, TResponse>, TResponse>
    {
        public async Task<TResponse> Handle(CreateRequest<TForm, TResponse> request, CancellationToken cancellationToken)
        {
            return await Create(request.Form, cancellationToken);
        }

        public abstract Task<TResponse> Create(TForm form, CancellationToken cancellationToken);
    }
}
