using GaEpd.AppLibrary.Domain.Entities;

namespace GaEpd.AppLibrary.Domain.Repositories;

/// <summary>
/// A generic repository for entities with methods for reading and writing data, with methods for reading and
/// mapping data to a destination type (DTO).
/// </summary>
/// <typeparam name="TEntity">The entity type.</typeparam>
/// <typeparam name="TKey">The primary key type for the entity.</typeparam>
public interface IRepositoryWithMapping<TEntity, in TKey>
    : IReadRepository<TEntity, TKey>, IReadRepositoryWithMapping<TEntity, TKey>, IWriteRepository<TEntity, TKey>
    where TEntity : IEntity<TKey>
    where TKey : IEquatable<TKey>;

/// <summary>
/// A generic repository for entities with <see cref="Guid"/> primary key with methods for reading and writing data,
/// including methods for reading and mapping data to a destination type (DTO).
/// </summary>
/// <typeparam name="TEntity">The entity type.</typeparam>
public interface IRepositoryWithMapping<TEntity>
    : IReadRepository<TEntity, Guid>, IReadRepositoryWithMapping<TEntity, Guid>, IWriteRepository<TEntity, Guid>
    where TEntity : IEntity<Guid>;
