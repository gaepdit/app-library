using AutoMapper;
using GaEpd.AppLibrary.Domain.Entities;
using System.Linq.Expressions;

namespace GaEpd.AppLibrary.Domain.Repositories;

/// <summary>
/// A repository for working with entities that have a <see cref="INamedEntity.Name"/> property, with methods for
/// reading and mapping data to a destination type (DTO).  
/// </summary>
/// <typeparam name="TEntity">The entity type.</typeparam>
public interface INamedEntityRepositoryWithMapping<TEntity>
    : INamedEntityRepository<TEntity>, IReadRepositoryWithMapping<TEntity, Guid>
    where TEntity : IEntity, INamedEntity
{
    /// <summary>
    /// Returns the <see cref="TEntity"/> with the given <paramref name="name"/>.
    /// Returns null if the name does not exist.
    /// </summary>
    /// <param name="name">The Name of the SimpleNamedEntity.</param>
    /// <param name="mapper">An instance of the <see cref="IMapper"/> defined in the consumer.</param>
    /// <param name="token"><see cref="T:System.Threading.CancellationToken"/></param>
    /// <returns>A SimpleNamedEntity entity.</returns>
    Task<TDestination?> FindByNameAsync<TDestination>(string name, IMapper mapper, CancellationToken token = default);

    /// <summary>
    /// Returns a read-only collection of all <see cref="INamedEntity"/> values ordered by <see cref="INamedEntity.Name"/>.
    /// Returns an empty collection if no entities exist.
    /// </summary>
    /// <param name="mapper">An instance of the <see cref="IMapper"/> defined in the consumer.</param>
    /// <param name="token"><see cref="T:System.Threading.CancellationToken"/></param>
    /// <returns>A read-only collection of entities.</returns>
    Task<IReadOnlyCollection<TDestination>> GetOrderedListAsync<TDestination>(IMapper mapper,
        CancellationToken token = default);

    /// <summary>
    /// Returns a read-only collection of all <see cref="INamedEntity"/> values matching the conditions of
    /// the <paramref name="predicate"/> and ordered by <see cref="INamedEntity.Name"/>.
    /// Returns an empty collection if there are no matches.
    /// </summary>
    /// <param name="predicate">The search conditions.</param>
    /// <param name="mapper">An instance of the <see cref="IMapper"/> defined in the consumer.</param>
    /// <param name="token"><see cref="T:System.Threading.CancellationToken"/></param>
    /// <returns>A read-only collection of entities.</returns>
    Task<IReadOnlyCollection<TDestination>> GetOrderedListAsync<TDestination>(Expression<Func<TEntity, bool>> predicate,
        IMapper mapper, CancellationToken token = default);
}
