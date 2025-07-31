using AutoMapper;
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
}
