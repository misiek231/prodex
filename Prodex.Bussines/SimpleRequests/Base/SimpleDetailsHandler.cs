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
}
