using AutoMapper;
using GaEpd.AppLibrary.Domain.Entities;
using System.Linq.Expressions;

namespace GaEpd.AppLibrary.Domain.Repositories.LocalRepository;

public abstract partial class BaseRepositoryWithMapping<TEntity, TKey>
    where TEntity : IEntity<TKey>
    where TKey : IEquatable<TKey>
{
    // FindAsync
    public Task<TDestination?> FindAsync<TDestination>(TKey id, IMapper mapper, CancellationToken token = default) =>
        FindAsync<TDestination>(entity => entity.Id.Equals(id), mapper, token);

    public async Task<TDestination?> FindAsync<TDestination>(Expression<Func<TEntity, bool>> predicate, IMapper mapper,
        CancellationToken token = default) =>
        await Task.FromResult(mapper.Map<TDestination?>(Items.SingleOrDefault(predicate.Compile())))
            .ConfigureAwait(false);
}
