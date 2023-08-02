namespace Prodex.Bussines.SimpleRequests.Base;

public interface IFilter<TEntity, TFilter>
{
    public abstract IQueryable<TEntity> Filter(IQueryable<TEntity> query, TFilter filterModel);
}
