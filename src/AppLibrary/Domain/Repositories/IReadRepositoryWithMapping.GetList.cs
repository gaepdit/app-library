using AutoMapper;
using System.Linq.Expressions;

namespace GaEpd.AppLibrary.Domain.Repositories;

public partial interface IReadRepositoryWithMapping<TEntity, in TKey>
{
    /// <summary>
    /// Returns a read-only collection of the <typeparamref name="TDestination"/> projection of all <see cref="TEntity"/> records.
    /// Returns an empty collection if there are no matches.
    /// </summary>
    /// <typeparam name="TDestination">The destination type.</typeparam>
    /// <param name="mapper">An instance of the <see cref="IMapper"/> defined in the consumer.</param>
    /// <param name="token"><see cref="T:System.Threading.CancellationToken"/></param>
    /// <returns>A read-only collection of entities.</returns>
    Task<IReadOnlyCollection<TDestination>> GetListAsync<TDestination>(IMapper mapper,
        CancellationToken token = default);

    /// <summary>
    /// Returns a read-only collection of the <typeparamref name="TDestination"/> projection of all <see cref="TEntity"/> records.
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
    /// <see cref="TEntity"/> records matching the conditions of the <paramref name="predicate"/>.
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
    /// <see cref="TEntity"/> records matching the conditions of the <paramref name="predicate"/>.
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
}
