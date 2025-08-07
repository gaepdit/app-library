using System.Linq.Expressions;

namespace GaEpd.AppLibrary.Domain.Repositories;

public partial interface IReadRepository<TEntity, in TKey>
{
    /// <summary>
    /// Returns a read-only collection of all <typeparamref name="TEntity"/> records.
    /// Returns an empty collection if there are no matches.
    /// </summary>
    /// <param name="token"><see cref="T:System.Threading.CancellationToken"/></param>
    /// <returns>A read-only collection of entities.</returns>
    Task<IReadOnlyCollection<TEntity>> GetListAsync(CancellationToken token = default);

    /// <summary>
    /// Returns a read-only collection of all <typeparamref name="TEntity"/> records.
    /// Returns an empty collection if there are no matches.
    /// </summary>
    /// <param name="ordering">An expression string to indicate values to order by.</param>
    /// <param name="token"><see cref="T:System.Threading.CancellationToken"/></param>
    /// <returns>A read-only collection of entities.</returns>
    Task<IReadOnlyCollection<TEntity>> GetListAsync(string ordering, CancellationToken token = default);

    /// <summary>
    /// Returns a read-only collection of all <typeparamref name="TEntity"/> records.
    /// Returns an empty collection if there are no matches.
    /// </summary>
    /// <param name="includeProperties">Navigation properties to include (when using an Entity Framework repository).</param>
    /// <param name="token"><see cref="T:System.Threading.CancellationToken"/></param>
    /// <returns>A read-only collection of entities.</returns>
    Task<IReadOnlyCollection<TEntity>> GetListAsync(string[] includeProperties, CancellationToken token = default);

    /// <summary>
    /// Returns a read-only collection of all <typeparamref name="TEntity"/> records.
    /// Returns an empty collection if there are no matches.
    /// </summary>
    /// <param name="ordering">An expression string to indicate values to order by.</param>
    /// <param name="includeProperties">Navigation properties to include (when using an Entity Framework repository).</param>
    /// <param name="token"><see cref="T:System.Threading.CancellationToken"/></param>
    /// <returns>A read-only collection of entities.</returns>
    Task<IReadOnlyCollection<TEntity>> GetListAsync(string ordering, string[] includeProperties,
        CancellationToken token = default);

    /// <summary>
    /// Returns a read-only collection of <typeparamref name="TEntity"/> matching the conditions of the <paramref name="predicate"/>.
    /// Returns an empty collection if there are no matches.
    /// </summary>
    /// <param name="predicate">The search conditions.</param>
    /// <param name="token"><see cref="T:System.Threading.CancellationToken"/></param>
    /// <returns>A read-only collection of entities.</returns>
    Task<IReadOnlyCollection<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predicate,
        CancellationToken token = default);

    /// <summary>
    /// Returns a read-only collection of <typeparamref name="TEntity"/> matching the conditions of the <paramref name="predicate"/>.
    /// Returns an empty collection if there are no matches.
    /// </summary>
    /// <param name="predicate">The search conditions.</param>
    /// <param name="ordering">An expression string to indicate values to order by.</param>
    /// <param name="token"><see cref="T:System.Threading.CancellationToken"/></param>
    /// <returns>A read-only collection of entities.</returns>
    Task<IReadOnlyCollection<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predicate, string ordering,
        CancellationToken token = default);

    /// <summary>
    /// Returns a read-only collection of <typeparamref name="TEntity"/> matching the conditions of the <paramref name="predicate"/>.
    /// Returns an empty collection if there are no matches.
    /// </summary>
    /// <param name="predicate">The search conditions.</param>
    /// <param name="includeProperties">Navigation properties to include (when using an Entity Framework repository).</param>
    /// <param name="token"><see cref="T:System.Threading.CancellationToken"/></param>
    /// <returns>A read-only collection of entities.</returns>
    Task<IReadOnlyCollection<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predicate,
        string[] includeProperties, CancellationToken token = default);

    /// <summary>
    /// Returns a read-only collection of <typeparamref name="TEntity"/> matching the conditions of the <paramref name="predicate"/>.
    /// Returns an empty collection if there are no matches.
    /// </summary>
    /// <param name="predicate">The search conditions.</param>
    /// <param name="ordering">An expression string to indicate values to order by.</param>
    /// <param name="includeProperties">Navigation properties to include (when using an Entity Framework repository).</param>
    /// <param name="token"><see cref="T:System.Threading.CancellationToken"/></param>
    /// <returns>A read-only collection of entities.</returns>
    Task<IReadOnlyCollection<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predicate, string ordering,
        string[] includeProperties, CancellationToken token = default);
}
