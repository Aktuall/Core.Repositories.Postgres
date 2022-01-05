using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;

namespace Aktuall.Core.Repositories.Postgres;

[PublicAPI]
public class Repository<TEntity, TKey> 
    where TKey : class
    where TEntity : Entity<TKey>
{
    protected readonly DbContext Context;
    protected readonly DbSet<TEntity> Set;
    protected readonly Validator<TEntity, TKey> Validator;

    protected Repository(DbContext context, DbSet<TEntity> set, Validator<TEntity, TKey> validator)
    {
        Context = context;
        Set = set;
        Validator = validator;
    }

    public virtual async Task<TEntity> InsertAsync(TEntity entity)
    {
        await Context.AddAsync(entity);
        await Context.SaveChangesAsync();
        return entity;
    }

    public virtual async Task<IReadOnlyList<TEntity>> InsertAsync(IEnumerable<TEntity> entities)
    {
        var entitiesList = entities.ToList();
        foreach (var entity in entitiesList)
        {
            await Context.AddAsync(entity);
        }

        await Context.SaveChangesAsync();
        return entitiesList.ToList();
    }

    public virtual async Task<TEntity> GetAsync(TKey id)
    {
        return await Context.FindAsync<TEntity>(id);
    }

    public virtual async Task<IReadOnlyList<TEntity>> GetAsync(Func<TEntity, bool> filterFunc)
    {
        var entities = await GetAllAsync();
        return entities.Where(filterFunc).ToList();
    }

    public virtual async Task RemoveAsync(TEntity entity)
    {
        Context.Remove(entity);
        await Context.SaveChangesAsync();
    }

    public virtual async Task<TEntity> UpdateAsync(TEntity entity)
    {
        Context.Entry(entity).State = EntityState.Modified;
        Context.Update(entity);
        await Context.SaveChangesAsync();
        return entity;
    }

    public virtual async Task<IReadOnlyList<TEntity>> GetAllAsync()
    {
        return await Set.ToListAsync();
    }

    protected void Validate(TEntity entity)
    {
        Validator.Validate(entity);
        Validator.CustomizeValidate(entity);
    }

    protected void Validate(IEnumerable<TEntity> entities)
    {
        foreach (var entity in entities) 
            Validate(entity);
    }
}