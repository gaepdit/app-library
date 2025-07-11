using GaEpd.AppLibrary.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;

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
    // FindAsync
    public Task<TEntity?> FindAsync(TKey id, CancellationToken token = default) =>
        FindAsyncInternal(id, token: token);

    public Task<TEntity?> FindAsync(TKey id, string[] includeProperties, CancellationToken token = default) =>
        FindAsyncInternal(id, includeProperties, token);

    public Task<TEntity?> FindAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken token = default) =>
        FindAsyncInternal(predicate, token: token);

    public Task<TEntity?> FindAsync(Expression<Func<TEntity, bool>> predicate, string[] includeProperties,
        CancellationToken token = default) =>
        FindAsyncInternal(predicate, includeProperties, token);

    private Task<TEntity?> FindAsyncInternal(TKey id, string[]? includeProperties = null,
        CancellationToken token = default) =>
        FindAsyncInternal(entity => entity.Id.Equals(id), includeProperties, token);

    private async Task<TEntity?> FindAsyncInternal(Expression<Func<TEntity, bool>> predicate,
        string[]? includeProperties = null, CancellationToken token = default) =>
        await NoTrackingSet(includeProperties).SingleOrDefaultAsync(predicate, token).ConfigureAwait(false);
}
