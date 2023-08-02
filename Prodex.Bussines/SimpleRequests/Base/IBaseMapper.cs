namespace Prodex.Bussines.SimpleRequests.Base;

public interface IListMapper<TEntity, TListItemModel>
{
    public abstract IQueryable<TListItemModel> ToListItemModel(IQueryable<TEntity> query);
}

public interface IDetailsMapper<TEntity, TDetails>
{
    public abstract IQueryable<TDetails> ToDetailsModel(IQueryable<TEntity> query);
}

public interface ICreateMapper<TEntity, TForm>
{
    public abstract TEntity ToEntity(TForm form);
}

public interface IUpdateMapper<TEntity, TForm>
{
    public abstract void ToEntity(TForm form, TEntity entity);
}