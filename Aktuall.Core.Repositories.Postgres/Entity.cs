using JetBrains.Annotations;

namespace Aktuall.Core.Repositories.Postgres
{
    [PublicAPI]
    public abstract class Entity<T> where T: class
    {
        public T Id { get; set; }
    }
}
