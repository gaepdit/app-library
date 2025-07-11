using GaEpd.AppLibrary.Domain.Entities;
using GaEpd.AppLibrary.Pagination;
using System.Linq.Expressions;

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

    // GetPagedListAsync
    public Task<IReadOnlyCollection<TEntity>> GetPagedListAsync(Expression<Func<TEntity, bool>> predicate,
        PaginatedRequest paging, CancellationToken token = default) =>
        GetPagedListAsyncInternal(predicate, paging);

    public Task<IReadOnlyCollection<TEntity>> GetPagedListAsync(Expression<Func<TEntity, bool>> predicate,
        PaginatedRequest paging, string[] includeProperties, CancellationToken token = default) =>
        GetPagedListAsyncInternal(predicate, paging);

    public Task<IReadOnlyCollection<TEntity>> GetPagedListAsync(PaginatedRequest paging,
        CancellationToken token = default) =>
        GetPagedListAsyncInternal(paging);

    public Task<IReadOnlyCollection<TEntity>> GetPagedListAsync(PaginatedRequest paging,
        string[] includeProperties, CancellationToken token = default) =>
        GetPagedListAsyncInternal(paging);

    private async Task<IReadOnlyCollection<TEntity>> GetPagedListAsyncInternal(
        Expression<Func<TEntity, bool>> predicate, PaginatedRequest paging) =>
        await Task.FromResult<IReadOnlyCollection<TEntity>>(Items.Where(predicate.Compile()).AsQueryable()
            .OrderByIf(paging.Sorting).Skip(paging.Skip).Take(paging.Take).ToList()).ConfigureAwait(false);

    private async Task<IReadOnlyCollection<TEntity>> GetPagedListAsyncInternal(PaginatedRequest paging) =>
        await Task.FromResult<IReadOnlyCollection<TEntity>>(Items.AsQueryable()
            .OrderByIf(paging.Sorting).Skip(paging.Skip).Take(paging.Take).ToList()).ConfigureAwait(false);
}
