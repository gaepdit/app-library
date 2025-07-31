using GaEpd.AppLibrary.Domain.Entities;

namespace GaEpd.AppLibrary.Domain.Repositories;

/// <summary>
/// A generic repository for Entities with methods for reading and mapping data to a destination type (DTO).
/// </summary>
/// <typeparam name="TEntity">The entity type.</typeparam>
/// <typeparam name="TKey">The primary key type for the entity.</typeparam>
public partial interface IReadRepositoryWithMapping<TEntity, in TKey> : IDisposable, IAsyncDisposable
    where TEntity : IEntity<TKey>
    where TKey : IEquatable<TKey>;
