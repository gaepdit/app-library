using AutoMapper;
using GaEpd.AppLibrary.Domain.Entities;
using System.Linq.Expressions;

namespace GaEpd.AppLibrary.Domain.Repositories.LocalRepository;

/// <summary>
/// An implementation of <see cref="IRepository{TEntity,TKey}"/> using in-memory data.
/// </summary>
/// <typeparam name="TEntity">The entity type.</typeparam>
/// <typeparam name="TKey">The primary key type for the entity.</typeparam>
/// <remarks>Navigation properties are already included when using in-memory data, so any `includeProperties`
/// parameters are ignored.</remarks>
public abstract partial class BaseRepositoryWithMapping<TEntity, TKey>
    where TEntity : IEntity<TKey>
    where TKey : IEquatable<TKey>
{
    // Navigation properties are already included when using in-memory data so are not used in the internal methods.

    // FindAsync
    public Task<TDestination?> FindAsync<TDestination>(TKey id, IMapper mapper, CancellationToken token = default) =>
        FindAsync<TDestination>(entity => entity.Id.Equals(id), mapper, token);

    public async Task<TDestination?> FindAsync<TDestination>(Expression<Func<TEntity, bool>> predicate, IMapper mapper,
        CancellationToken token = default) =>
        await Task.FromResult(mapper.Map<TDestination?>(Items.SingleOrDefault(predicate.Compile())))
            .ConfigureAwait(false);
}
