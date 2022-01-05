namespace Aktuall.Core.Repositories.Postgres;

public class Validator<TEntity, TKey>
    where TKey : class
    where TEntity : Entity<TKey>
{
    public void Validate(TEntity entity)
    {
        if (entity.Id == null)
            throw new ValidationException("Id cannot be null");
    }

    public virtual void CustomizeValidate(TEntity entity)
    {

    }
}