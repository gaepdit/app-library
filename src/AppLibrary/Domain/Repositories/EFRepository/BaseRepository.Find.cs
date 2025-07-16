using AutoMapper;
using AutoMapper.QueryableExtensions;
using GaEpd.AppLibrary.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;

namespace GaEpd.AppLibrary.Domain.Repositories.EFRepository;

/// <summary>
/// An implementation of <see cref="IRepository{TEntity,TKey}"/> using Entity Framework.
/// </summary>
/// <typeparam name="TEntity">The entity type.</typeparam>
/// <typeparam name="TKey">The primary key type for the entity.</typeparam>
/// <typeparam name="TContext">The type of the <see cref="DbContext"/>.</typeparam>
[SuppressMessage("", "S2436")]
public abstract partial class BaseRepository<TEntity, TKey, TContext>
    where TEntity : class, IEntity<TKey>
    where TKey : IEquatable<TKey>
    where TContext : DbContext
{
    // FindAsync
    public Task<TEntity?> FindAsync(TKey id, CancellationToken token = default) =>
        FindInternal(id, token: token);

    public Task<TEntity?> FindAsync(TKey id, string[] includeProperties, CancellationToken token = default) =>
        FindInternal(id, includeProperties, token);

    public Task<TEntity?> FindAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken token = default) =>
        FindInternal(predicate, token: token);

    public Task<TEntity?> FindAsync(Expression<Func<TEntity, bool>> predicate, string[] includeProperties,
        CancellationToken token = default) =>
        FindInternal(predicate, includeProperties, token);

    public Task<TDestination?> FindAsync<TDestination>(TKey id, IMapper mapper, CancellationToken token = default) =>
        FindAsync<TDestination>(entity => entity.Id.Equals(id), mapper, token);

    public async Task<TDestination?> FindAsync<TDestination>(Expression<Func<TEntity, bool>> predicate, IMapper mapper,
        CancellationToken token = default) =>
        await Context.Set<TEntity>().AsNoTracking()
            .Where(predicate)
            .ProjectTo<TDestination>(mapper.ConfigurationProvider)
            .SingleOrDefaultAsync(token).ConfigureAwait(false);

    // Internal methods

    private Task<TEntity?> FindInternal(TKey id, string[]? includeProperties = null,
        CancellationToken token = default) =>
        FindInternal(entity => entity.Id.Equals(id), includeProperties, token);

    private async Task<TEntity?> FindInternal(Expression<Func<TEntity, bool>> predicate,
        string[]? includeProperties = null, CancellationToken token = default) =>
        await NoTrackingSet(includeProperties).SingleOrDefaultAsync(predicate, token).ConfigureAwait(false);
}
