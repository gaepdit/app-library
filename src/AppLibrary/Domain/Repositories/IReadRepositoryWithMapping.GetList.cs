using AutoMapper;
using System.Linq.Expressions;

namespace GaEpd.AppLibrary.Domain.Repositories;

public partial interface IReadRepositoryWithMapping<TEntity, in TKey>
{
    // =========== Direct mapping methods

    /// <summary>
    /// Returns a read-only collection of the <typeparamref name="TDestination"/> projection of all
    /// <typeparamref name="TEntity"/> records.
    /// Returns an empty collection if there are no matches.
    /// </summary>
    /// <typeparam name="TDestination">The destination type.</typeparam>
    /// <param name="mapper">An instance of the <see cref="IMapper"/> defined in the consumer.</param>
    /// <param name="token"><see cref="T:System.Threading.CancellationToken"/></param>
    /// <returns>A read-only collection of entities.</returns>
    Task<IReadOnlyCollection<TDestination>> GetListAsync<TDestination>(IMapper mapper,
        CancellationToken token = default);

    /// <summary>
    /// Returns a read-only collection of the <typeparamref name="TDestination"/> projection of all
    /// <typeparamref name="TEntity"/> records.
    /// Returns an empty collection if there are no matches.
    /// </summary>
    /// <typeparam name="TDestination">The destination type.</typeparam>
    /// <param name="ordering">An expression string to indicate values to order by.</param>
    /// <param name="mapper">An instance of the <see cref="IMapper"/> defined in the consumer.</param>
    /// <param name="token"><see cref="T:System.Threading.CancellationToken"/></param>
    /// <returns>A read-only collection of entities.</returns>
    Task<IReadOnlyCollection<TDestination>> GetListAsync<TDestination>(string ordering, IMapper mapper,
        CancellationToken token = default);

    /// <summary>
    /// Returns a read-only collection of the <typeparamref name="TDestination"/> projection of all
    /// <typeparamref name="TEntity"/> records matching the conditions of the <paramref name="predicate"/>.
    /// Returns an empty collection if there are no matches.
    /// </summary>
    /// <typeparam name="TDestination">The destination type.</typeparam>
    /// <param name="predicate">The search conditions.</param>
    /// <param name="mapper">An instance of the <see cref="IMapper"/> defined in the consumer.</param>
    /// <param name="token"><see cref="T:System.Threading.CancellationToken"/></param>
    /// <returns>A read-only collection of entities.</returns>
    Task<IReadOnlyCollection<TDestination>> GetListAsync<TDestination>(Expression<Func<TEntity, bool>> predicate,
        IMapper mapper, CancellationToken token = default);

    /// <summary>
    /// Returns a read-only collection of the <typeparamref name="TDestination"/> projection of all
    /// <typeparamref name="TEntity"/> records matching the conditions of the <paramref name="predicate"/>.
    /// Returns an empty collection if there are no matches.
    /// </summary>
    /// <typeparam name="TDestination">The destination type.</typeparam>
    /// <param name="predicate">The search conditions.</param>
    /// <param name="ordering">An expression string to indicate values to order by.</param>
    /// <param name="mapper">An instance of the <see cref="IMapper"/> defined in the consumer.</param>
    /// <param name="token"><see cref="T:System.Threading.CancellationToken"/></param>
    /// <returns>A read-only collection of entities.</returns>
    Task<IReadOnlyCollection<TDestination>> GetListAsync<TDestination>(Expression<Func<TEntity, bool>> predicate,
        string ordering, IMapper mapper, CancellationToken token = default);


    // =========== Mapping from derived type

    /// <summary>
    /// Returns a read-only collection of the <typeparamref name="TDestination"/> projection of all
    /// <typeparamref name="TSource"/> records,
    /// where <typeparamref name="TSource"/> is a child class derived from <typeparamref name="TEntity"/>.
    /// Returns an empty collection if there are no matches.
    /// </summary>
    /// <typeparam name="TDestination">The destination type.</typeparam>
    /// <typeparam name="TSource">The derived type to filter the results on.</typeparam>
    /// <param name="mapper">An instance of the <see cref="IMapper"/> defined in the consumer.</param>
    /// <param name="token"><see cref="T:System.Threading.CancellationToken"/></param>
    /// <returns>A read-only collection of entities.</returns>
    Task<IReadOnlyCollection<TDestination>> GetListAsync<TDestination, TSource>(IMapper mapper,
        CancellationToken token = default)
        where TSource : TEntity;

    /// <summary>
    /// Returns a read-only collection of the <typeparamref name="TDestination"/> projection of all
    /// <typeparamref name="TSource"/> records,
    /// where <typeparamref name="TSource"/> is a child class derived from <typeparamref name="TEntity"/>.
    /// Returns an empty collection if there are no matches.
    /// </summary>
    /// <typeparam name="TDestination">The destination type.</typeparam>
    /// <typeparam name="TSource">The derived type to filter the results on.</typeparam>
    /// <param name="ordering">An expression string to indicate values to order by.</param>
    /// <param name="mapper">An instance of the <see cref="IMapper"/> defined in the consumer.</param>
    /// <param name="token"><see cref="T:System.Threading.CancellationToken"/></param>
    /// <returns>A read-only collection of entities.</returns>
    Task<IReadOnlyCollection<TDestination>> GetListAsync<TDestination, TSource>(string ordering, IMapper mapper,
        CancellationToken token = default)
        where TSource : TEntity;

    /// <summary>
    /// Returns a read-only collection of the <typeparamref name="TDestination"/> projection of all
    /// <typeparamref name="TSource"/> records matching the conditions of the <paramref name="predicate"/>,
    /// where <typeparamref name="TSource"/> is a child class derived from <typeparamref name="TEntity"/>.
    /// Returns an empty collection if there are no matches.
    /// </summary>
    /// <typeparam name="TDestination">The destination type.</typeparam>
    /// <typeparam name="TSource">The derived type to filter the results on.</typeparam>
    /// <param name="predicate">The search conditions.</param>
    /// <param name="mapper">An instance of the <see cref="IMapper"/> defined in the consumer.</param>
    /// <param name="token"><see cref="T:System.Threading.CancellationToken"/></param>
    /// <returns>A read-only collection of entities.</returns>
    Task<IReadOnlyCollection<TDestination>> GetListAsync<TDestination, TSource>(
        Expression<Func<TSource, bool>> predicate, IMapper mapper, CancellationToken token = default)
        where TSource : TEntity;

    /// <summary>
    /// Returns a read-only collection of the <typeparamref name="TDestination"/> projection of all
    /// <typeparamref name="TSource"/> records matching the conditions of the <paramref name="predicate"/>,
    /// where <typeparamref name="TSource"/> is a child class derived from <typeparamref name="TEntity"/>.
    /// Returns an empty collection if there are no matches.
    /// </summary>
    /// <typeparam name="TDestination">The destination type.</typeparam>
    /// <typeparam name="TSource">The derived type to filter the results on.</typeparam>
    /// <param name="predicate">The search conditions.</param>
    /// <param name="ordering">An expression string to indicate values to order by.</param>
    /// <param name="mapper">An instance of the <see cref="IMapper"/> defined in the consumer.</param>
    /// <param name="token"><see cref="T:System.Threading.CancellationToken"/></param>
    /// <returns>A read-only collection of entities.</returns>
    Task<IReadOnlyCollection<TDestination>> GetListAsync<TDestination, TSource>(
        Expression<Func<TSource, bool>> predicate, string ordering, IMapper mapper, CancellationToken token = default)
        where TSource : TEntity;
}
