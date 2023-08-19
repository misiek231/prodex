using MediatR;
using Microsoft.EntityFrameworkCore;
using Prodex.Data;
using Prodex.Data.Interfaces;

namespace Prodex.Bussines.SimpleRequests.Base;

public class SimpleGetDetails
{
    public class Request<TEntity, TDetails> : IRequest<TDetails>
        where TEntity : class
    {
        public long Id { get; set; }

        public Request(long id)
        {
            Id = id;
        }
    }

    public class Handler<TEntity, TDetails> : IRequestHandler<Request<TEntity, TDetails>, TDetails>
        where TEntity : class, IEntity
    {
        private readonly IDetailsMapper<TEntity, TDetails> Mapper;
        private readonly DataContext Context;

        public Handler(IDetailsMapper<TEntity, TDetails> mapper, DataContext context)
        {
            Mapper = mapper;
            Context = context;
        }

        public async Task<TDetails> Handle(Request<TEntity, TDetails> request, CancellationToken cancellationToken)
        {
            return await Context.Set<TEntity>()
                .Where(p => p.Id == request.Id)
                .ToDetailsModel(Mapper)
                .FirstOrDefaultAsync(cancellationToken);
        }
    }

    public class AfterFetchRequest<TDetails> : IRequest<TDetails>
    {
        public TDetails Model { get; set; }

        public AfterFetchRequest<TDetails> Init(TDetails model)
        {
            Model = model;
            return this;
        }
    }

    public class Request<TEntity, TDetails, TAfterFetchRequest> : IRequest<TDetails>
    where TEntity : class
    where TAfterFetchRequest : AfterFetchRequest<TDetails>
    {
        public long Id { get; set; }

        public Request(long id)
        {
            Id = id;
        }
    }

    public class Handler<TEntity, TDetails, TAfterFetchRequest> : IRequestHandler<Request<TEntity, TDetails, TAfterFetchRequest>, TDetails>
    where TEntity : class, IEntity
    where TAfterFetchRequest : AfterFetchRequest<TDetails>, new()
    {
        private readonly IDetailsMapper<TEntity, TDetails> Mapper;
        private readonly IMediator Mediator;
        private readonly DataContext Context;

        public Handler(IDetailsMapper<TEntity, TDetails> mapper, DataContext context, IMediator mediator)
        {
            Mapper = mapper;
            Context = context;
            Mediator = mediator;
        }

        public async Task<TDetails> Handle(Request<TEntity, TDetails, TAfterFetchRequest> request, CancellationToken cancellationToken)
        {
            var result = await Context.Set<TEntity>()
                .Where(p => p.Id == request.Id)
                .ToDetailsModel(Mapper)
                .FirstOrDefaultAsync(cancellationToken);

            return await Mediator.Send(new TAfterFetchRequest().Init(result), cancellationToken);
        }
    }
}
