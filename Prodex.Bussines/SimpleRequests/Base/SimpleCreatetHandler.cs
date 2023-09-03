using MediatR;
using Prodex.Data;
using Prodex.Data.Interfaces;

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

    public interface IAfterCreateRequest<TEnity> : IRequest<object>
        where TEnity : IEntity
    {
        public TEnity Enity { get; set; }
    }

    public class Handler<TEntity, TForm, TAfterCreateRequest> : IRequestHandler<Request<TEntity, TForm>, object>
    where TEntity : class, IEntity
    where TAfterCreateRequest : IAfterCreateRequest<TEntity>, new()
    {
        private readonly ICreateMapper<TEntity, TForm> Mapper;
        private readonly DataContext Context;
        private readonly IMediator Mediator;


        public Handler(ICreateMapper<TEntity, TForm> mapper, DataContext context, IMediator mediator)
        {
            Mapper = mapper;
            Context = context;
            Mediator = mediator;
        }

        public async Task<object> Handle(Request<TEntity, TForm> request, CancellationToken cancellationToken)
        {
            var entity = Mapper.ToEntity(request.Form);
            Context.Add(entity);
            await Context.SaveChangesAsync(cancellationToken);

            await Mediator.Send(new TAfterCreateRequest() { Enity = entity }, cancellationToken);

            return null; // Todo: return detils model
        }
    }
}
