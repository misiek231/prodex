﻿using FluentValidation;
using Prodex.Shared.Forms;
using Prodex.Shared.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Prodex.Shared.Models.Processes;

public class FormModel : FormBaseModel<FormModel>
{
    [Description("Nazwa")]
    public string Name { get; set; }
    public string Xml { get; set; }

    public override void Rules(ValidationContext validationContext, FluentValidator<FormModel> model)
    {
        model.RuleFor(p => p.Name).NotEmpty().LengthCheck(60);
    }
}
