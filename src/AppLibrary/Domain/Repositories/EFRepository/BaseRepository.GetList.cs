using GaEpd.AppLibrary.Domain.Entities;
using GaEpd.AppLibrary.Pagination;
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
    // GetListAsync
    public Task<IReadOnlyCollection<TEntity>> GetListAsync(CancellationToken token = default) =>
        GetListAsyncInternal(token: token);

    public Task<IReadOnlyCollection<TEntity>> GetListAsync(string ordering, CancellationToken token = default) =>
        GetListAsyncInternal(ordering, token: token);

    public Task<IReadOnlyCollection<TEntity>> GetListAsync(string[] includeProperties,
        CancellationToken token = default) =>
        GetListAsyncInternal(includeProperties: includeProperties, token: token);

    public Task<IReadOnlyCollection<TEntity>> GetListAsync(string ordering, string[] includeProperties,
        CancellationToken token = default) =>
        GetListAsyncInternal(ordering, includeProperties, token);

    public Task<IReadOnlyCollection<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predicate,
        CancellationToken token = default) =>
        GetListAsyncInternal(predicate, token: token);

    public Task<IReadOnlyCollection<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predicate, string ordering,
        CancellationToken token = default) =>
        GetListAsyncInternal(predicate, ordering, token: token);

    public Task<IReadOnlyCollection<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predicate,
        string[] includeProperties, CancellationToken token = default) =>
        GetListAsyncInternal(predicate, includeProperties: includeProperties, token: token);

    public Task<IReadOnlyCollection<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predicate,
        string ordering, string[] includeProperties, CancellationToken token = default) =>
        GetListAsyncInternal(predicate, ordering, includeProperties, token);

    private async Task<IReadOnlyCollection<TEntity>> GetListAsyncInternal(string? ordering = null,
        string[]? includeProperties = null, CancellationToken token = default) =>
        await NoTrackingSet(includeProperties).OrderByIf(ordering).ToListAsync(token).ConfigureAwait(false);

    private async Task<IReadOnlyCollection<TEntity>> GetListAsyncInternal(Expression<Func<TEntity, bool>> predicate,
        string? ordering = null, string[]? includeProperties = null, CancellationToken token = default) =>
        await NoTrackingSet(includeProperties).Where(predicate).OrderByIf(ordering).ToListAsync(token)
            .ConfigureAwait(false);
}
