using FluentValidation;

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
