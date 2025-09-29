using AutoMapper;
using GaEpd.AppLibrary.Pagination;
using System.Linq.Expressions;

namespace GaEpd.AppLibrary.Domain.Repositories;

public partial interface IReadRepositoryWithMapping<TEntity, in TKey>
{
    /// <summary>
    /// Returns a paginated read-only collection of the <typeparamref name="TDestination"/> projection of all
    /// <typeparamref name="TEntity"/> records.
    /// Returns an empty collection if there are no matches.
    /// </summary>
    /// <typeparam name="TDestination">The destination type.</typeparam>
    /// <param name="paging">A <see cref="PaginatedRequest"/> to define the paging options.</param>
    /// <param name="mapper">An instance of the <see cref="IMapper"/> defined in the consumer.</param>
    /// <param name="token"><see cref="T:System.Threading.CancellationToken"/></param>
    /// <returns>A sorted and paged read-only collection of entities.</returns>
    Task<IReadOnlyCollection<TDestination>> GetPagedListAsync<TDestination>(PaginatedRequest paging, IMapper mapper,
        CancellationToken token = default);

    /// <summary>
    /// Returns a paginated read-only collection of the <typeparamref name="TDestination"/> projection of all
    /// <typeparamref name="TEntity"/> records matching the conditions of the <paramref name="predicate"/>.
    /// </summary>
    /// <typeparam name="TDestination">The destination type.</typeparam>
    /// <param name="predicate">The search conditions.</param>
    /// <param name="paging">A <see cref="PaginatedRequest"/> to define the paging options.</param>
    /// <param name="mapper">An instance of the <see cref="IMapper"/> defined in the consumer.</param>
    /// <param name="token"><see cref="T:System.Threading.CancellationToken"/></param>
    /// <returns>A sorted and paged read-only collection of entities.</returns>
    Task<IReadOnlyCollection<TDestination>> GetPagedListAsync<TDestination>(Expression<Func<TEntity, bool>> predicate,
        PaginatedRequest paging, IMapper mapper, CancellationToken token = default);

    /// <summary>
    /// Returns a paginated read-only collection of the <typeparamref name="TDestination"/> projection of all
    /// <typeparamref name="TEntity"/> records.
    /// Returns an empty collection if there are no matches.
    /// </summary>
    /// <typeparam name="TDestination">The destination type.</typeparam>
    /// <typeparam name="TSource">The derived type to filter the results on.</typeparam>
    /// <param name="paging">A <see cref="PaginatedRequest"/> to define the paging options.</param>
    /// <param name="mapper">An instance of the <see cref="IMapper"/> defined in the consumer.</param>
    /// <param name="token"><see cref="T:System.Threading.CancellationToken"/></param>
    /// <returns>A sorted and paged read-only collection of entities.</returns>
    Task<IReadOnlyCollection<TDestination>> GetPagedListAsync<TDestination, TSource>(PaginatedRequest paging,
        IMapper mapper, CancellationToken token = default)
        where TSource : TEntity;

    /// <summary>
    /// Returns a paginated read-only collection of the <typeparamref name="TDestination"/> projection of all
    /// <typeparamref name="TEntity"/> records matching the conditions of the <paramref name="predicate"/>.
    /// </summary>
    /// <typeparam name="TDestination">The destination type.</typeparam>
    /// <typeparam name="TSource">The derived type to filter the results on.</typeparam>
    /// <param name="predicate">The search conditions.</param>
    /// <param name="paging">A <see cref="PaginatedRequest"/> to define the paging options.</param>
    /// <param name="mapper">An instance of the <see cref="IMapper"/> defined in the consumer.</param>
    /// <param name="token"><see cref="T:System.Threading.CancellationToken"/></param>
    /// <returns>A sorted and paged read-only collection of entities.</returns>
    Task<IReadOnlyCollection<TDestination>> GetPagedListAsync<TDestination, TSource>(
        Expression<Func<TSource, bool>> predicate, PaginatedRequest paging, IMapper mapper,
        CancellationToken token = default)
        where TSource : TEntity;
}
