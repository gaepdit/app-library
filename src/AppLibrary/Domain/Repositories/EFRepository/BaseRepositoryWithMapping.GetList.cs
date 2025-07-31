using AutoMapper;
using GaEpd.AppLibrary.Domain.Entities;
using GaEpd.AppLibrary.Extensions;
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
    // GetListAsync
    public Task<IReadOnlyCollection<TDestination>> GetListAsync<TDestination>(IMapper mapper,
        CancellationToken token = default) =>
        GetListInternal<TDestination>(mapper, token: token);

    public Task<IReadOnlyCollection<TDestination>> GetListAsync<TDestination>(string ordering, IMapper mapper,
        CancellationToken token = default) =>
        GetListInternal<TDestination>(mapper, ordering: ordering, token: token);

    public Task<IReadOnlyCollection<TDestination>> GetListAsync<TDestination>(Expression<Func<TEntity, bool>> predicate,
        IMapper mapper, CancellationToken token = default) =>
        GetListInternal<TDestination>(mapper, predicate, token: token);

    public Task<IReadOnlyCollection<TDestination>> GetListAsync<TDestination>(Expression<Func<TEntity, bool>> predicate,
        string ordering, IMapper mapper, CancellationToken token = default) =>
        GetListInternal<TDestination>(mapper, predicate, ordering, token);

    // Internal methods
    private async Task<IReadOnlyCollection<TDestination>> GetListInternal<TDestination>(IMapper mapper,
        Expression<Func<TEntity, bool>>? predicate = null, string? ordering = null,
        CancellationToken token = default) =>
        await mapper.ProjectTo<TDestination>(source: NoTrackingSet()
            .WhereIf(predicate)
            .OrderByIf(ordering)
        ).ToListAsync(token).ConfigureAwait(false);
}
