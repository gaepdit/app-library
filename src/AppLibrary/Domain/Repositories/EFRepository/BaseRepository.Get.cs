using GaEpd.AppLibrary.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GaEpd.AppLibrary.Domain.Repositories.EFRepository;

public abstract partial class BaseRepository<TEntity, TKey, TContext>
    where TEntity : class, IEntity<TKey>
    where TKey : IEquatable<TKey>
    where TContext : DbContext
{
    // GetAsync
    public Task<TEntity> GetAsync(TKey id, CancellationToken token = default) =>
        GetInternal(id, token: token);

    public Task<TEntity> GetAsync(TKey id, string[] includeProperties, CancellationToken token = default) =>
        GetInternal(id, includeProperties, token);

    // Internal methods
    private async Task<TEntity> GetInternal(TKey id, string[]? includeProperties = null,
        CancellationToken token = default) =>
        await TrackingSet(includeProperties).SingleOrDefaultAsync(entity => entity.Id.Equals(id), token)
            .ConfigureAwait(false) ?? throw new EntityNotFoundException<TEntity>(id);
}
