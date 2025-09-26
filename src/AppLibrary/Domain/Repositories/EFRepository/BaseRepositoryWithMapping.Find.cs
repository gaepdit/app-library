using AutoMapper;
using GaEpd.AppLibrary.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace GaEpd.AppLibrary.Domain.Repositories.EFRepository;

public abstract partial class BaseRepositoryWithMapping<TEntity, TKey, TContext>
    where TEntity : class, IEntity<TKey>
    where TKey : IEquatable<TKey>
    where TContext : DbContext
{
    // FindAsync
    public Task<TDestination?> FindAsync<TDestination>(TKey id, IMapper mapper, CancellationToken token = default) =>
        FindAsync<TDestination>(entity => entity.Id.Equals(id), mapper, token);

    public async Task<TDestination?> FindAsync<TDestination>(Expression<Func<TEntity, bool>> predicate, IMapper mapper,
        CancellationToken token = default) =>
        await mapper.ProjectTo<TDestination>(source: NoTrackingSet()
            .Where(predicate)
        ).SingleOrDefaultAsync(token).ConfigureAwait(false);

    public Task<TDestination?> FindAsync<TDestination, TSource>(TKey id, IMapper mapper,
        CancellationToken token = default) where TSource : TEntity =>
        FindAsync<TDestination, TSource>(source => source.Id.Equals(id), mapper, token);

    public async Task<TDestination?> FindAsync<TDestination, TSource>(Expression<Func<TSource, bool>> predicate,
        IMapper mapper, CancellationToken token = default) where TSource : TEntity =>
        await mapper.ProjectTo<TDestination>(source: NoTrackingSet()
            .OfType<TSource>()
            .Where(predicate)
        ).SingleOrDefaultAsync(token).ConfigureAwait(false);
}
