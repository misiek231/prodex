namespace Prodex.Bussines.SimpleRequests.Base;

public static class IQueryableExtensions
{
    public static IQueryable<TListItemModel> ToListItemModel<TEntity, TListItemModel>(this IQueryable<TEntity> enities, IListMapper<TEntity, TListItemModel> mapper)
    {
        return mapper.ToListItemModel(enities);
    }

    public static IQueryable<TDetailsModel> ToDetailsModel<TEntity, TDetailsModel>(this IQueryable<TEntity> enities, IDetailsMapper<TEntity, TDetailsModel> mapper)
    {
        return mapper.ToDetailsModel(enities);
    }

    public static IQueryable<TEntity> Filter<TEntity, TFilter>(this IQueryable<TEntity> enities, IFilter<TEntity, TFilter> filter, TFilter model)
    {
        return filter.Filter(enities, model);
    }
}
