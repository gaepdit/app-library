namespace GaEpd.AppLibrary.Domain.Repositories;

public partial interface IReadRepository<TEntity, in TKey>
{
    /// <summary>
    /// Returns the <typeparamref name="TEntity"/> with the given <paramref name="id"/>.
    /// </summary>
    /// <param name="id">The ID of the entity.</param>
    /// <param name="token"><see cref="T:System.Threading.CancellationToken"/></param>
    /// <exception cref="EntityNotFoundException{TEntity}">Thrown if no entity exists with the given ID.</exception>
    /// <returns>An entity.</returns>
    Task<TEntity> GetAsync(TKey id, CancellationToken token = default);

    /// <summary>
    /// Returns the <typeparamref name="TEntity"/> with the given <paramref name="id"/>.
    /// </summary>
    /// <param name="id">The ID of the entity.</param>
    /// <param name="includeProperties">Navigation properties to include (when using an Entity Framework repository).</param>
    /// <param name="token"><see cref="T:System.Threading.CancellationToken"/></param>
    /// <exception cref="EntityNotFoundException{TEntity}">Thrown if no entity exists with the given ID.</exception>
    /// <returns>An entity.</returns>
    Task<TEntity> GetAsync(TKey id, string[] includeProperties, CancellationToken token = default);
}
