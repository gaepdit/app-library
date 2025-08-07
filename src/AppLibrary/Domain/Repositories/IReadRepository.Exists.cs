using System.Linq.Expressions;

namespace GaEpd.AppLibrary.Domain.Repositories;

public partial interface IReadRepository<TEntity, in TKey>
{
    /// <summary>
    /// Returns a boolean indicating whether an <typeparamref name="TEntity"/> with the given <paramref name="id"/> exists.
    /// </summary>
    /// <param name="id">The ID of the entity.</param>
    /// <param name="token"><see cref="T:System.Threading.CancellationToken"/></param>
    /// <returns>True if the entity exists. Otherwise, false.</returns>
    public Task<bool> ExistsAsync(TKey id, CancellationToken token = default);

    /// <summary>
    /// Returns a boolean indicating whether an <typeparamref name="TEntity"/> exists matching the conditions of
    /// the <paramref name="predicate"/>.
    /// </summary>
    /// <param name="predicate">The search conditions.</param>
    /// <param name="token"><see cref="T:System.Threading.CancellationToken"/></param>
    /// <returns>True if the entity exists. Otherwise, false.</returns>
    public Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken token = default);
}
