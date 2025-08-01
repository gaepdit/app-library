using GaEpd.AppLibrary.Domain.Entities;

namespace GaEpd.AppLibrary.Domain.Repositories.LocalRepository;

/// <summary>
/// An implementation of <see cref="IRepositoryWithMapping{TEntity,TKey}"/> using in-memory data where TKey is
/// a <see cref="Guid"/> primary key.
/// </summary>
/// <typeparam name="TEntity">The entity type.</typeparam>
/// <remarks>Navigation properties are already included when using in-memory data structures,
/// so any `includeProperties` parameters are ignored.</remarks>
public abstract class BaseRepositoryWithMapping<TEntity>(IEnumerable<TEntity> items)
    : BaseRepositoryWithMapping<TEntity, Guid>(items), IRepositoryWithMapping<TEntity>
    where TEntity : class, IEntity;

/// <summary>
/// An implementation of <see cref="IRepositoryWithMapping{TEntity,TKey}"/> using in-memory data.
/// </summary>
/// <typeparam name="TEntity">The entity type.</typeparam>
/// <typeparam name="TKey">The primary key type for the entity.</typeparam>
/// <remarks>Navigation properties are already included when using in-memory data structures,
/// so any `includeProperties` parameters are ignored.</remarks>
public abstract partial class BaseRepositoryWithMapping<TEntity, TKey>(IEnumerable<TEntity> items)
    : BaseRepository<TEntity, TKey>(items), IRepositoryWithMapping<TEntity, TKey>
    where TEntity : IEntity<TKey>
    where TKey : IEquatable<TKey> { }
