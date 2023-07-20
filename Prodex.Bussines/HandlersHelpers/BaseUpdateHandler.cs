using MediatR;
using Prodex.Bussines.Requests;

namespace Prodex.Bussines.HandlersHelpers
{
    public abstract class BaseUpdateHandler<TForm, TResponse> : IRequestHandler<UpdateRequest<TForm, TResponse>, TResponse>
    {
        public async Task<TResponse> Handle(UpdateRequest<TForm, TResponse> request, CancellationToken cancellationToken)
        {
            return await Update(request.Id, request.Form, cancellationToken);
        }

        public abstract Task<TResponse> Update(long id, TForm form, CancellationToken cancellationToken);
    }
}
