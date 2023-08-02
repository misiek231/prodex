using MediatR;
using Prodex.Data;

namespace Prodex.Bussines.SimpleRequests.Base;

public class SimpleUpdate
{
    public class Request<TEntity, TForm> : IRequest<object>
        where TEntity : class
    {
        public long Id { get; set; }
        public TForm Form { get; set; }

        public Request(long id, TForm form)
        {
            Id = id;
            Form = form;
        }
    }

    public class Handler<TEntity, TForm> : IRequestHandler<Request<TEntity, TForm>, object>
    where TEntity : class
    {
        private readonly IUpdateMapper<TEntity, TForm> Mapper;
        private readonly DataContext Context;

        public Handler(IUpdateMapper<TEntity, TForm> mapper, DataContext context)
        {
            Mapper = mapper;
            Context = context;
        }

        public async Task<object> Handle(Request<TEntity, TForm> request, CancellationToken cancellationToken)
        {
            var entity = await Context.Set<TEntity>().FindAsync(new object[] { request.Id }, cancellationToken: cancellationToken);
            Mapper.ToEntity(request.Form, entity);
            await Context.SaveChangesAsync(cancellationToken);
            return null; // Todo: return detils model
        }
    }
}
