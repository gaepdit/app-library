using GaEpd.AppLibrary.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace GaEpd.AppLibrary.Domain.Repositories.EFRepository;

/// <summary>
/// An implementation of <see cref="IRepository{TEntity,TKey}"/> using Entity Framework.
/// </summary>
/// <typeparam name="TEntity">The entity type.</typeparam>
/// <typeparam name="TKey">The primary key type for the entity.</typeparam>
/// <typeparam name="TContext">The type of the <see cref="DbContext"/>.</typeparam>
[SuppressMessage("", "S2436")]
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
