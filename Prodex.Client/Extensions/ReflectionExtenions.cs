using System.Linq.Expressions;

namespace Prodex.Client.Extensions;

public static class ReflectionExtenions
{
    public static void SetValue<TModel, TValue>(this Expression<Func<TModel, TValue>> memberExpression, TModel form, TValue newVlaue)
    {
        var body = (MemberExpression)memberExpression.Body;
        var newValueParam = Expression.Parameter(typeof(TValue));
        var formParam = memberExpression.Parameters.Single();
        var assignExpression = Expression.Assign(body, Expression.Convert(newValueParam, body.Type));

        var assign = Expression.Lambda<Action<TModel, TValue>>(assignExpression, formParam, newValueParam).Compile();
        assign(form, newVlaue);
    }
}
