using AutoMapper;
using GaEpd.AppLibrary.Domain.Entities;
using GaEpd.AppLibrary.Extensions;
using GaEpd.AppLibrary.Pagination;
using System.Linq.Dynamic.Core;
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

    public Task<IReadOnlyCollection<TDestination>> GetPagedListAsync<TDestination>(
        Expression<Func<TEntity, bool>> predicate, PaginatedRequest paging, IMapper mapper,
        CancellationToken token = default) =>
        GetPagedListInternal<TDestination>(mapper, paging, predicate);

    public Task<IReadOnlyCollection<TDestination>> GetPagedListAsync<TDestination>(PaginatedRequest paging,
        IMapper mapper, CancellationToken token = default) =>
        GetPagedListInternal<TDestination>(mapper, paging);

    // Internal methods
    private async Task<IReadOnlyCollection<TEntity>> GetPagedListInternal(PaginatedRequest paging,
        Expression<Func<TEntity, bool>>? predicate = null) =>
        await Task.FromResult<IReadOnlyCollection<TEntity>>(Items.WhereIf(predicate).OrderBy(paging.Sorting)
            .Skip(paging.Skip).Take(paging.Take).ToList()).ConfigureAwait(false);

    private async Task<IReadOnlyCollection<TDestination>> GetPagedListInternal<TDestination>(IMapper mapper,
        PaginatedRequest paging, Expression<Func<TEntity, bool>>? predicate = null) =>
        await Task.FromResult(mapper.Map<IReadOnlyCollection<TDestination>>(Items.WhereIf(predicate)
            .OrderBy(paging.Sorting)
            .Skip(paging.Skip).Take(paging.Take).ToList())).ConfigureAwait(false);
}
