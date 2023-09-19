using Prodex.Shared.Forms;

namespace Prodex.Bussines.SimpleRequests.Base;

public interface IValidatorUpdate<TEntity, TModel>
{
    ValidationErrors ValidateUpdate(TEntity entity, TModel model);
}

public interface IValidatorCreate<TModel>
{
    ValidationErrors ValidateCreate(TModel model);
}

public interface IValidator<TEntity, TModel> : IValidatorCreate<TModel>, IValidatorUpdate<TEntity, TModel>
{
}
