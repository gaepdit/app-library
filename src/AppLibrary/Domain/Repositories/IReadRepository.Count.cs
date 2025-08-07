using System.Linq.Expressions;

namespace GaEpd.AppLibrary.Domain.Repositories;

public partial interface IReadRepository<TEntity, in TKey>
{
    /// <summary>
    /// Returns the count of <typeparamref name="TEntity"/> matching the conditions of the <paramref name="predicate"/>.
    /// Returns zero if there are no matches.
    /// </summary>
    /// <param name="predicate">The search conditions.</param>
    /// <param name="token"><see cref="T:System.Threading.CancellationToken"/></param>
    /// <returns>The number of matching entities.</returns>
    public Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken token = default);
}
