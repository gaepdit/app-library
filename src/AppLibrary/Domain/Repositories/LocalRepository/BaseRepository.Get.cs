using GaEpd.AppLibrary.Domain.Entities;

namespace GaEpd.AppLibrary.Domain.Repositories.LocalRepository;

/// <summary>
/// An implementation of <see cref="IRepository{TEntity,TKey}"/> using in-memory data.
/// </summary>
/// <typeparam name="TEntity">The entity type.</typeparam>
/// <typeparam name="TKey">The primary key type for the entity.</typeparam>
/// <remarks>Navigation properties are already included when using in-memory data structures,
/// so any `includeProperties` parameters are ignored.</remarks>
public abstract partial class BaseRepository<TEntity, TKey>
    where TEntity : IEntity<TKey>
    where TKey : IEquatable<TKey>
{
    // Navigation properties are already included when using in-memory data structures so are not used in the internal methods.

    // GetAsync
    public Task<TEntity> GetAsync(TKey id, CancellationToken token = default) =>
        GetAsyncInternal(id);

    public Task<TEntity> GetAsync(TKey id, string[] includeProperties, CancellationToken token = default) =>
        GetAsyncInternal(id);

    private async Task<TEntity> GetAsyncInternal(TKey id) =>
        await FindAsyncInternal(id).ConfigureAwait(false) ??
        throw new EntityNotFoundException<TEntity>(id);
}
