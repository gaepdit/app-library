using AutoMapper;
using GaEpd.AppLibrary.Pagination;
using System.Linq.Expressions;

namespace GaEpd.AppLibrary.Domain.Repositories;

public partial interface IReadRepository<TEntity, in TKey>
{
    /// <summary>
    /// Returns a filtered, read-only collection of <see cref="TEntity"/> matching the conditions of the
    /// <paramref name="predicate"/>. Returns an empty collection if there are no matches.
    /// </summary>
    /// <param name="predicate">The search conditions.</param>
    /// <param name="paging">A <see cref="PaginatedRequest"/> to define the paging options.</param>
    /// <param name="token"><see cref="T:System.Threading.CancellationToken"/></param>
    /// <returns>A sorted and paged read-only collection of entities.</returns>
    Task<IReadOnlyCollection<TEntity>> GetPagedListAsync(Expression<Func<TEntity, bool>> predicate,
        PaginatedRequest paging, CancellationToken token = default);

    /// <summary>
    /// Returns a filtered, read-only collection of <see cref="TEntity"/> matching the conditions of the
    /// <paramref name="predicate"/>. Returns an empty collection if there are no matches.
    /// </summary>
    /// <param name="predicate">The search conditions.</param>
    /// <param name="paging">A <see cref="PaginatedRequest"/> to define the paging options.</param>
    /// <param name="includeProperties">Navigation properties to include (when using an Entity Framework repository).</param>
    /// <param name="token"><see cref="T:System.Threading.CancellationToken"/></param>
    /// <returns>A sorted and paged read-only collection of entities.</returns>
    Task<IReadOnlyCollection<TEntity>> GetPagedListAsync(Expression<Func<TEntity, bool>> predicate,
        PaginatedRequest paging, string[] includeProperties, CancellationToken token = default);

    /// <summary>
    /// Returns a filtered, read-only collection of all <see cref="TEntity"/> values.
    /// Returns an empty collection if there are no matches.
    /// </summary>
    /// <param name="paging">A <see cref="PaginatedRequest"/> to define the paging options.</param>
    /// <param name="token"><see cref="T:System.Threading.CancellationToken"/></param>
    /// <returns>A sorted and paged read-only collection of entities.</returns>
    Task<IReadOnlyCollection<TEntity>> GetPagedListAsync(PaginatedRequest paging, CancellationToken token = default);

    /// <summary>
    /// Returns a filtered, read-only collection of all <see cref="TEntity"/> values.
    /// Returns an empty collection if there are no matches.
    /// </summary>
    /// <param name="paging">A <see cref="PaginatedRequest"/> to define the paging options.</param>
    /// <param name="includeProperties">Navigation properties to include (when using an Entity Framework repository).</param>
    /// <param name="token"><see cref="T:System.Threading.CancellationToken"/></param>
    /// <returns>A sorted and paged read-only collection of entities.</returns>
    Task<IReadOnlyCollection<TEntity>> GetPagedListAsync(PaginatedRequest paging, string[] includeProperties,
        CancellationToken token = default);

    /// <summary>
    /// Returns a filtered, read-only collection of <see cref="TEntity"/> matching the conditions of the
    /// <paramref name="predicate"/>. Returns an empty collection if there are no matches.
    /// </summary>
    /// <param name="predicate">The search conditions.</param>
    /// <param name="paging">A <see cref="PaginatedRequest"/> to define the paging options.</param>
    /// <param name="mapper">An instance of the <see cref="IMapper"/> defined in the consumer.</param>
    /// <param name="token"><see cref="T:System.Threading.CancellationToken"/></param>
    /// <returns>A sorted and paged read-only collection of entities.</returns>
    Task<IReadOnlyCollection<TDestination>> GetPagedListAsync<TDestination>(Expression<Func<TEntity, bool>> predicate,
        PaginatedRequest paging,
        IMapper mapper, CancellationToken token = default);

    /// <summary>
    /// Returns a filtered, read-only collection of all <see cref="TEntity"/> values.
    /// Returns an empty collection if there are no matches.
    /// </summary>
    /// <param name="paging">A <see cref="PaginatedRequest"/> to define the paging options.</param>
    /// <param name="mapper">An instance of the <see cref="IMapper"/> defined in the consumer.</param>
    /// <param name="token"><see cref="T:System.Threading.CancellationToken"/></param>
    /// <returns>A sorted and paged read-only collection of entities.</returns>
    Task<IReadOnlyCollection<TDestination>> GetPagedListAsync<TDestination>(PaginatedRequest paging, IMapper mapper,
        CancellationToken token = default);
}
