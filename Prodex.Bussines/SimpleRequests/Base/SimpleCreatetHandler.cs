using MediatR;
using Prodex.Data;

namespace Prodex.Bussines.SimpleRequests.Base;

public class SimpleCreate
{

    public class Request<TEntity, TForm> : IRequest<object>
        where TEntity : class
    {
        public TForm Form { get; set; }

        public Request(TForm form)
        {
            Form = form;
        }
    }

    public class Handler<TEntity, TForm> : IRequestHandler<Request<TEntity, TForm>, object>
    where TEntity : class
    {
        private readonly ICreateMapper<TEntity, TForm> Mapper;
        private readonly DataContext Context;

        public Handler(ICreateMapper<TEntity, TForm> mapper, DataContext context)
        {
            Mapper = mapper;
            Context = context;
        }

        public async Task<object> Handle(Request<TEntity, TForm> request, CancellationToken cancellationToken)
        {
            Context.Add(Mapper.ToEntity(request.Form));
            await Context.SaveChangesAsync(cancellationToken);
            return null; // Todo: return detils model
        }
    }
}
