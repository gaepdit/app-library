using GaEpd.AppLibrary.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace GaEpd.AppLibrary.Domain.Repositories.EFRepository;

public abstract partial class BaseRepository<TEntity, TKey, TContext>
    where TEntity : class, IEntity<TKey>
    where TKey : IEquatable<TKey>
    where TContext : DbContext
{
    // FindAsync
    public Task<TEntity?> FindAsync(TKey id, CancellationToken token = default) =>
        FindInternal(id, includeProperties: null, token);

    public Task<TEntity?> FindAsync(TKey id, string[] includeProperties, CancellationToken token = default) =>
        FindInternal(id, includeProperties, token);

    public Task<TEntity?> FindAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken token = default) =>
        FindInternal(predicate, includeProperties: null, token);

    public Task<TEntity?> FindAsync(Expression<Func<TEntity, bool>> predicate, string[] includeProperties,
        CancellationToken token = default) =>
        FindInternal(predicate, includeProperties, token);

    // Internal methods
    private Task<TEntity?> FindInternal(TKey id, string[]? includeProperties, CancellationToken token) =>
        FindInternal(entity => entity.Id.Equals(id), includeProperties, token);

    private async Task<TEntity?> FindInternal(Expression<Func<TEntity, bool>> predicate, string[]? includeProperties,
        CancellationToken token = default) =>
        await NoTrackingSet(includeProperties).SingleOrDefaultAsync(predicate, token).ConfigureAwait(false);
}
