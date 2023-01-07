﻿using FluentValidation.Validators;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prodex.Shared.Validation
{
    public static class Extensions
    {
        public static IRuleBuilderOptions<T, string> LengthCheck<T>(this IRuleBuilder<T, string> builder, int maximumLength)
        {
            return builder.MaximumLength(maximumLength).WithMessage($"Maksymalna długość pola to {maximumLength} znaków");
        }
    }
}
