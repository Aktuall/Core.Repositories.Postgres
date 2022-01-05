using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Aktuall.Core.Repositories.Postgres;

[PublicAPI]
public sealed class PostgresRepositoriesBuilder
{
    private readonly IServiceCollection services;
    private readonly DbContext context;

    public PostgresRepositoriesBuilder(IServiceCollection services, DbContext context)
    {
        this.services = services;
        this.context = context;
    }

    public IServiceCollection AddKeyValidator<TEntity, TKey>()
        where TEntity : Entity<TKey>
        where TKey : class
    {
        services.AddSingleton<AbstractValidator<TEntity, TKey>>();

        return services;
    }

    public IServiceCollection AddRepository<TEntity, TKey>()
        where TEntity : Entity<TKey>
        where TKey : class
    {
        services.AddSingleton(context.Set<TEntity>());
        services.AddSingleton<AbstractRepository<TEntity, TKey>>();

        return services;
    }
}