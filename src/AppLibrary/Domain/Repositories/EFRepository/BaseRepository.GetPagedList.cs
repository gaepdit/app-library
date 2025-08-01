using GaEpd.AppLibrary.Domain.Entities;
using GaEpd.AppLibrary.Extensions;
using GaEpd.AppLibrary.Pagination;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace GaEpd.AppLibrary.Domain.Repositories.EFRepository;

public abstract partial class BaseRepository<TEntity, TKey, TContext>
    where TEntity : class, IEntity<TKey>
    where TKey : IEquatable<TKey>
    where TContext : DbContext
{
    // GetPagedListAsync
    public Task<IReadOnlyCollection<TEntity>> GetPagedListAsync(Expression<Func<TEntity, bool>> predicate,
        PaginatedRequest paging, CancellationToken token = default) =>
        GetPagedListInternal(paging, predicate, token: token);

    public Task<IReadOnlyCollection<TEntity>> GetPagedListAsync(Expression<Func<TEntity, bool>> predicate,
        PaginatedRequest paging, string[] includeProperties, CancellationToken token = default) =>
        GetPagedListInternal(paging, predicate, includeProperties, token);

    public Task<IReadOnlyCollection<TEntity>> GetPagedListAsync(PaginatedRequest paging,
        CancellationToken token = default) =>
        GetPagedListInternal(paging, token: token);

    public Task<IReadOnlyCollection<TEntity>> GetPagedListAsync(PaginatedRequest paging, string[] includeProperties,
        CancellationToken token = default) =>
        GetPagedListInternal(paging, includeProperties: includeProperties, token: token);

    // Internal methods
    private async Task<IReadOnlyCollection<TEntity>> GetPagedListInternal(PaginatedRequest paging,
        Expression<Func<TEntity, bool>>? predicate = null, string[]? includeProperties = null,
        CancellationToken token = default) =>
        await NoTrackingSet(includeProperties)
            .WhereIf(predicate)
            .ApplyPaging(paging)
            .ToListAsync(token).ConfigureAwait(false);
}
