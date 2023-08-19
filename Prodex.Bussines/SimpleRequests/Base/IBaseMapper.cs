namespace Prodex.Bussines.SimpleRequests.Base;

public interface IListMapper<TEntity, TListItemModel>
{
    IQueryable<TListItemModel> ToListItemModel(IQueryable<TEntity> query);
}

public interface IDetailsMapper<TEntity, TDetails>
{
    IQueryable<TDetails> ToDetailsModel(IQueryable<TEntity> query);
    TDetails ToDetailsModel(TEntity model);
}

public interface ICreateMapper<TEntity, TForm>
{
    TEntity ToEntity(TForm form);
}

public interface IUpdateMapper<TEntity, TForm>
{
    void ToEntity(TForm form, TEntity entity);
}