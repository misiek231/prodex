using MediatR;
using Microsoft.EntityFrameworkCore;
using Prodex.Bussines.Mappers;
using Prodex.Bussines.SimpleRequests.Base;
using Prodex.Data;
using Prodex.Data.Models;
using Prodex.Shared.Models.ProductTemplates.FieldsConfig;

namespace Prodex.Bussines.Handlers.ProductTemplatesConfigs;

public static class UpdateFieldsConfigHandler
{

    public class Request : IRequest<FieldsConfigFormModel>
    {
        public long TemplateId { get; set; }
        public FieldsConfigFormModel Model { get; set; }

        public Request(long templateId, FieldsConfigFormModel model)
        {
            TemplateId = templateId;
            Model = model;
        }
    }

    public class Handler : IRequestHandler<Request, FieldsConfigFormModel>
    {
        private readonly DataContext context;
        private readonly FieldsConfigMapper mapper;

        public Handler(DataContext context, ICreateMapper<FieldConfig, FieldModel> mapper)
        {
            this.context = context;
            this.mapper = (FieldsConfigMapper)mapper;
        }

        public async Task<FieldsConfigFormModel> Handle(Request request, CancellationToken cancellationToken)
        {
            var existing = await context.FieldConfigs
                .Include(p => p.DynamicFieldValues)
                .Where(p => p.TemplateId == request.TemplateId)
                .ToListAsync(cancellationToken);

            var toDelete = existing.Where(p => !request.Model.Fields.Any(q => q.Id == p.Id));

            context.RemoveRange(toDelete.SelectMany(p => p.DynamicFieldValues));
            context.RemoveRange(toDelete);

            foreach(var field in request.Model.Fields)
            {
                if (field.Id.HasValue)
                {
                    existing.Single(p => p.Id == field.Id).DisplayName = field.DisplayName;
                }
                else
                {
                    var newField = mapper.ToEntity(field);
                    newField.TemplateId = request.TemplateId;
                    context.Add(newField);
                }
            } 

            //var entities = mapper.ToEntity(request.Model.Fields).ToList();

            //entities.ForEach(p => p.TemplateId = request.TemplateId);

           // await context.AddRangeAsync(entities, cancellationToken);

            await context.SaveChangesAsync(cancellationToken);

            return request.Model;
        }
    }
}