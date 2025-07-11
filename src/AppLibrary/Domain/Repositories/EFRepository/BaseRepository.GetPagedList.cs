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
    // GetPagedListAsync
    public Task<IReadOnlyCollection<TEntity>> GetPagedListAsync(Expression<Func<TEntity, bool>> predicate,
        PaginatedRequest paging, CancellationToken token = default) =>
        GetPagedListAsyncInternal(predicate, paging, token: token);

    public Task<IReadOnlyCollection<TEntity>> GetPagedListAsync(Expression<Func<TEntity, bool>> predicate,
        PaginatedRequest paging, string[] includeProperties, CancellationToken token = default) =>
        GetPagedListAsyncInternal(predicate, paging, includeProperties, token);

    public Task<IReadOnlyCollection<TEntity>> GetPagedListAsync(PaginatedRequest paging,
        CancellationToken token = default) =>
        GetPagedListAsyncInternal(paging, token: token);

    public Task<IReadOnlyCollection<TEntity>> GetPagedListAsync(PaginatedRequest paging, string[] includeProperties,
        CancellationToken token = default) =>
        GetPagedListAsyncInternal(paging, includeProperties, token);

    private async Task<IReadOnlyCollection<TEntity>> GetPagedListAsyncInternal(
        Expression<Func<TEntity, bool>> predicate, PaginatedRequest paging, string[]? includeProperties = null,
        CancellationToken token = default) =>
        await ApplyPagingAsync(NoTrackingSet(includeProperties).Where(predicate), paging, token).ConfigureAwait(false);

    private async Task<IReadOnlyCollection<TEntity>> GetPagedListAsyncInternal(PaginatedRequest paging,
        string[]? includeProperties = null, CancellationToken token = default) =>
        await ApplyPagingAsync(NoTrackingSet(includeProperties), paging, token).ConfigureAwait(false);

    private static Task<List<TEntity>> ApplyPagingAsync(IQueryable<TEntity> queryable, PaginatedRequest paging,
        CancellationToken token) =>
        queryable.OrderByIf(paging.Sorting).Skip(paging.Skip).Take(paging.Take).ToListAsync(token);
}
