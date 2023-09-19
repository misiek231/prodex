using MediatR;
using Microsoft.Extensions.DependencyInjection;
using OneOf;
using Prodex.Data;
using Prodex.Shared.Forms;

namespace Prodex.Bussines.SimpleRequests.Base;

public class SimpleUpdate
{
    public class Request<TEntity, TForm> : IRequest<OneOf<TEntity, ValidationErrors>>
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

    public class Handler<TEntity, TForm> : IRequestHandler<Request<TEntity, TForm>, OneOf<TEntity, ValidationErrors>>
    where TEntity : class
    {
        private readonly IUpdateMapper<TEntity, TForm> Mapper;
        private readonly IValidatorUpdate<TEntity, TForm> Validator;
        private readonly DataContext Context;

        public Handler(IUpdateMapper<TEntity, TForm> mapper, DataContext context, IServiceProvider services)
        {
            Mapper = mapper;
            Context = context;
            Validator = services.GetService<IValidatorUpdate<TEntity, TForm>>();
        }

        public async Task<OneOf<TEntity, ValidationErrors>> Handle(Request<TEntity, TForm> request, CancellationToken cancellationToken)
        {
            var entity = await Context.Set<TEntity>().FindAsync(new object[] { request.Id }, cancellationToken: cancellationToken);

            var validationResult = Validator?.ValidateUpdate(entity, request.Form);

            if (validationResult?.HasErrors == true)
            {
                return validationResult;
            }

            Mapper.ToEntity(request.Form, entity);

            await Context.SaveChangesAsync(cancellationToken);
            
            return entity;
        }
    }
}
