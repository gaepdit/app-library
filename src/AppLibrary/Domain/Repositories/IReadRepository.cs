using GaEpd.AppLibrary.Domain.Entities;

namespace GaEpd.AppLibrary.Domain.Repositories;

/// <summary>
/// A generic repository for Entities with methods for reading data.
/// </summary>
/// <typeparam name="TEntity">The entity type.</typeparam>
/// <typeparam name="TKey">The primary key type for the entity.</typeparam>
public partial interface IReadRepository<TEntity, in TKey> : IDisposable, IAsyncDisposable
    where TEntity : IEntity<TKey>
    where TKey : IEquatable<TKey>;
