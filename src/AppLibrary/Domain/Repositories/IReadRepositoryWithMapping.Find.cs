using AutoMapper;
using System.Linq.Expressions;

namespace GaEpd.AppLibrary.Domain.Repositories;

public partial interface IReadRepositoryWithMapping<TEntity, in TKey>
{
    /// <summary>
    /// Returns the <typeparamref name="TDestination"/> projection of the entity matching the
    /// given <paramref name="id"/>.
    /// Returns null if there are no matches.
    /// </summary>
    /// <typeparam name="TDestination">The destination type.</typeparam>
    /// <param name="id">The ID of the entity.</param>
    /// <param name="mapper">An instance of the <see cref="IMapper"/> defined in the consumer.</param>
    /// <param name="token"><see cref="T:System.Threading.CancellationToken"/></param>
    /// <exception cref="InvalidOperationException">Thrown if there are multiple matches.</exception>
    /// <returns>An entity or null.</returns>
    Task<TDestination?> FindAsync<TDestination>(TKey id, IMapper mapper, CancellationToken token = default);

    /// <summary>
    /// Returns the <typeparamref name="TDestination"/> projection of the entity matching the conditions of
    /// the <paramref name="predicate"/>.
    /// Returns null if there are no matches.
    /// </summary>
    /// <typeparam name="TDestination">The destination type.</typeparam>
    /// <param name="predicate">The search conditions.</param>
    /// <param name="mapper">An instance of the <see cref="IMapper"/> defined in the consumer.</param>
    /// <param name="token"><see cref="T:System.Threading.CancellationToken"/></param>
    /// <exception cref="InvalidOperationException">Thrown if there are multiple matches.</exception>
    /// <returns>An entity or null.</returns>
    Task<TDestination?> FindAsync<TDestination>(Expression<Func<TEntity, bool>> predicate, IMapper mapper,
        CancellationToken token = default);
}
