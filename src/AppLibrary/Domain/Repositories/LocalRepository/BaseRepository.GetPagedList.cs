using GaEpd.AppLibrary.Domain.Entities;
using GaEpd.AppLibrary.Extensions;
using GaEpd.AppLibrary.Pagination;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;

namespace GaEpd.AppLibrary.Domain.Repositories.LocalRepository;

public abstract partial class BaseRepository<TEntity, TKey>
    where TEntity : IEntity<TKey>
    where TKey : IEquatable<TKey>
{
    // GetPagedListAsync
    public Task<IReadOnlyCollection<TEntity>> GetPagedListAsync(Expression<Func<TEntity, bool>> predicate,
        PaginatedRequest paging, CancellationToken token = default) =>
        GetPagedListInternal(paging, predicate);

    public Task<IReadOnlyCollection<TEntity>> GetPagedListAsync(Expression<Func<TEntity, bool>> predicate,
        PaginatedRequest paging, string[] includeProperties, CancellationToken token = default) =>
        GetPagedListInternal(paging, predicate);

    public Task<IReadOnlyCollection<TEntity>> GetPagedListAsync(PaginatedRequest paging,
        CancellationToken token = default) =>
        GetPagedListInternal(paging);

    public Task<IReadOnlyCollection<TEntity>> GetPagedListAsync(PaginatedRequest paging,
        string[] includeProperties, CancellationToken token = default) =>
        GetPagedListInternal(paging);

    // Internal methods
    private async Task<IReadOnlyCollection<TEntity>> GetPagedListInternal(PaginatedRequest paging,
        Expression<Func<TEntity, bool>>? predicate = null) =>
        await Task.FromResult<IReadOnlyCollection<TEntity>>(Items.WhereIf(predicate).OrderBy(paging.Sorting)
            .Skip(paging.Skip).Take(paging.Take).ToList()).ConfigureAwait(false);
}
