using System;
using JetBrains.Annotations;

namespace Aktuall.Core.Repositories.Postgres
{
    [PublicAPI]
    public abstract class Entity<T> where T: class
    {
        public T Id { get; }

        protected Entity(T id)
        {
            Id = id ?? throw new ArgumentNullException(nameof(id), "Entity's id cannot be null");
        }
    }
}
