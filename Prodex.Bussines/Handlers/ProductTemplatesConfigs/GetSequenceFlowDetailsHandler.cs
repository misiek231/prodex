﻿using MediatR;
using Microsoft.EntityFrameworkCore;
using Prodex.Bussines.SimpleRequests.Base;
using Prodex.Data;
using Prodex.Data.Models;
using Prodex.Shared.Models.Dictionary;
using Prodex.Shared.Models.ProductTemplates;
using Prodex.Shared.Models.ProductTemplates.ElementOptions;
using System.Linq;

namespace Prodex.Bussines.Handlers.ProductTemplatesConfigs;


public static class GetSequenceFlowDetails
{

    public class Request : IRequest<SequenceFlowConfigFormModel>
    {
        public long TemplateId { get; set; }
        public string FlowId { get; set; }

        public Request(long templateId, string flowId)
        {
            TemplateId = templateId;
            FlowId = flowId;
        }
    }

    public class Handler : IRequestHandler<Request, SequenceFlowConfigFormModel>
    {
        private readonly DataContext context;

        public Handler(DataContext context)
        {
            this.context = context;
        }

        public async Task<SequenceFlowConfigFormModel> Handle(Request request, CancellationToken cancellationToken)
        {
            var result = await context.SequenceFlowConfigs
                .Where(p => p.TemplateId == request.TemplateId && p.FlowId == request.FlowId)
                .Select(p => new SequenceFlowConfigFormModel
                {
                    Id = p.Id,
                    LOperandType = p.LdynamicField.HasValue ? OperandType.DynamicField : p.LDictionaryTermId.HasValue ? OperandType.DictionaryTerm : OperandType.Value,
                    LdynamicField = p.LdynamicField.HasValue ? new ApiFieldConfig(p.LdynamicFieldNavigation.Id, p.LdynamicFieldNavigation.DisplayName) : null,
                    LDictionaryField = p.LDictionaryTermId.HasValue ? new ApiDictionaryTermSelect(p.LDictionaryTerm.Id, p.LDictionaryTerm.Value) : null,
                    Lvalue = p.Lvalue,
                    OperatorType = (OperatorType)p.OperatorType,
                    ROperandType = p.RdynamicField.HasValue ? OperandType.DynamicField : p.RDictionaryTermId.HasValue ? OperandType.DictionaryTerm : OperandType.Value,
                    RdynamicField = p.RdynamicField.HasValue ? new ApiFieldConfig(p.RdynamicFieldNavigation.Id, p.RdynamicFieldNavigation.DisplayName) : null,
                    RDictionaryField = p.RDictionaryTermId.HasValue ? new ApiDictionaryTermSelect(p.RDictionaryTerm.Id, p.RDictionaryTerm.Value) : null,
                    Rvalue = p.Rvalue,
                }).FirstOrDefaultAsync(cancellationToken);

            if (result == null) return null;

            return result;
        }
    }
}