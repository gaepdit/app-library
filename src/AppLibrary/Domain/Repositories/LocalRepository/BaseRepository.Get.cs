using GaEpd.AppLibrary.Domain.Entities;

namespace GaEpd.AppLibrary.Domain.Repositories.LocalRepository;

public abstract partial class BaseRepository<TEntity, TKey>
    where TEntity : IEntity<TKey>
    where TKey : IEquatable<TKey>
{
    // GetAsync
    public Task<TEntity> GetAsync(TKey id, CancellationToken token = default) =>
        GetInternal(id);

    public Task<TEntity> GetAsync(TKey id, string[] includeProperties, CancellationToken token = default) =>
        GetInternal(id);

    // Internal methods
    private async Task<TEntity> GetInternal(TKey id) =>
        await FindInternal(id).ConfigureAwait(false) ??
        throw new EntityNotFoundException<TEntity>(id);
}
