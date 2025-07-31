using AutoMapper;
using GaEpd.AppLibrary.Domain.Entities;
using System.Linq.Expressions;

namespace GaEpd.AppLibrary.Domain.Repositories.LocalRepository;

/// <summary>
/// An implementation of <see cref="INamedEntityRepository{TEntity}"/> using in-memory data. The implementation is
/// derived from <see cref="BaseRepository{TEntity,TKey}"/> and uses a <see cref="Guid"/> for the Entity primary key.
/// </summary>
/// <typeparam name="TEntity">The entity type.</typeparam>
public abstract class NamedEntityRepositoryWithMapping<TEntity>(IEnumerable<TEntity> items)
    : BaseRepositoryWithMapping<TEntity>(items), INamedEntityRepositoryWithMapping<TEntity>
    where TEntity : class, IEntity, INamedEntity
{
    public async Task<TEntity?> FindByNameAsync(string name, CancellationToken token = default) =>
        await Task.FromResult(Items.SingleOrDefault(entity =>
            string.Equals(entity.Name, name, StringComparison.OrdinalIgnoreCase))).ConfigureAwait(false);

    public async Task<IReadOnlyCollection<TEntity>> GetOrderedListAsync(CancellationToken token = default) =>
        await Task.FromResult<IReadOnlyCollection<TEntity>>(Items.OrderBy(entity => entity.Name)
            .ThenBy(entity => entity.Id).ToList()).ConfigureAwait(false);

    public async Task<IReadOnlyCollection<TEntity>> GetOrderedListAsync(Expression<Func<TEntity, bool>> predicate,
        CancellationToken token = default) =>
        await Task.FromResult<IReadOnlyCollection<TEntity>>(Items.Where(predicate.Compile())
            .OrderBy(entity => entity.Name).ThenBy(entity => entity.Id).ToList()).ConfigureAwait(false);

    public async Task<TDestination?> FindByNameAsync<TDestination>(string name, IMapper mapper,
        CancellationToken token = default) =>
        await Task.FromResult(mapper.Map<TDestination?>(Items.SingleOrDefault(entity =>
            string.Equals(entity.Name, name, StringComparison.OrdinalIgnoreCase)))).ConfigureAwait(false);

    public async Task<IReadOnlyCollection<TDestination>> GetOrderedListAsync<TDestination>(IMapper mapper,
        CancellationToken token = default) =>
        await Task.FromResult(mapper.Map<IReadOnlyCollection<TDestination>>(Items.OrderBy(entity => entity.Name)
            .ThenBy(entity => entity.Id).ToList())).ConfigureAwait(false);

    public async Task<IReadOnlyCollection<TDestination>> GetOrderedListAsync<TDestination>(
        Expression<Func<TEntity, bool>> predicate, IMapper mapper, CancellationToken token = default) =>
        await Task.FromResult(mapper.Map<IReadOnlyCollection<TDestination>>(Items.Where(predicate.Compile())
            .OrderBy(entity => entity.Name).ThenBy(entity => entity.Id).ToList())).ConfigureAwait(false);
}
