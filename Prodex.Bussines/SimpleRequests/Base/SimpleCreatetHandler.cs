using MediatR;
using Microsoft.Extensions.DependencyInjection;
using OneOf;
using Prodex.Data;
using Prodex.Data.Interfaces;
using Prodex.Shared.Forms;

namespace Prodex.Bussines.SimpleRequests.Base;

public class SimpleCreate
{

    public class Request<TEntity, TForm> : IRequest<OneOf<TEntity, ValidationErrors>>
        where TEntity : class, IEntity
    {
        public TForm Form { get; set; }
        public long UserId { get; set; }

        public Request(TForm form, long userId)
        {
            Form = form;
            UserId = userId;
        }
    }

    public class Handler<TEntity, TForm> : IRequestHandler<Request<TEntity, TForm>, OneOf<TEntity, ValidationErrors>>
    where TEntity : class, IEntity
    {
        private readonly ICreateMapper<TEntity, TForm> Mapper;
        private readonly IValidatorCreate<TForm> Validator;
        private readonly DataContext Context;

        public Handler(ICreateMapper<TEntity, TForm> mapper, DataContext context, IServiceProvider services)
        {
            Mapper = mapper;
            Context = context;
            Validator = services.GetService<IValidatorCreate<TForm>>();
        }

        public async Task<OneOf<TEntity, ValidationErrors>> Handle(Request<TEntity, TForm> request, CancellationToken cancellationToken)
        {
            if (Validator is not null)
            {
                var validationResult = Validator.ValidateCreate(request.Form);

                if (validationResult.HasErrors)
                {
                    return validationResult;
                }
            }

            var entity = Mapper.ToEntity(request.Form);

            if (entity is IConfidential conf)
            {
                conf.DateCreatedUtc = DateTime.UtcNow;
                conf.DateUpdatedUtc = DateTime.UtcNow;
                conf.CreatedBy = request.UserId;
                conf.UpdatedBy = request.UserId;
            }

            Context.Add(entity);
            await Context.SaveChangesAsync(cancellationToken);

            return entity;
        }
    }

    public interface IAfterCreateRequest<TEnity> : IRequest<object>
        where TEnity : IEntity
    {
        public TEnity Enity { get; set; }
    }

    public class Handler<TEntity, TForm, TAfterCreateRequest> : IRequestHandler<Request<TEntity, TForm>, OneOf<TEntity, ValidationErrors>>
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

        public async Task<OneOf<TEntity, ValidationErrors>> Handle(Request<TEntity, TForm> request, CancellationToken cancellationToken)
        {
            var entity = Mapper.ToEntity(request.Form);

            if (entity is IConfidential conf)
            {
                conf.DateCreatedUtc = DateTime.UtcNow;
                conf.DateUpdatedUtc = DateTime.UtcNow;
                conf.CreatedBy = request.UserId;
                conf.UpdatedBy = request.UserId;
            }

            Context.Add(entity);
            await Context.SaveChangesAsync(cancellationToken);

            await Mediator.Send(new TAfterCreateRequest() { Enity = entity }, cancellationToken);

            return entity;
        }
    }
}
