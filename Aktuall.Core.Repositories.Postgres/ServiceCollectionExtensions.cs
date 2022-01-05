﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aktuall.Core.Repositories.Postgres.Configuration;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Aktuall.Core.Repositories.Postgres;

[PublicAPI]
public static class ServiceCollectionExtensions
{
    public static PostgresRepositoriesBuilder AddPostgresRepositories(this IServiceCollection services, IConfiguration configuration)
    {
        var postgresConfig = configuration.Get<PostgresConfig>();
        services.AddSingleton<DbContext>();

        var contextOptionsBuilder = new DbContextOptionsBuilder();
        contextOptionsBuilder.UseNpgsql(postgresConfig.ConnectionString);

        var context = new DbContext(contextOptionsBuilder.Options);

        var builder = new PostgresRepositoriesBuilder(services, context);

        return builder;
    }
}