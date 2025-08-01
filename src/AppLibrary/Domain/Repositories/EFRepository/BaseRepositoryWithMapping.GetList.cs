using AutoMapper;
using GaEpd.AppLibrary.Domain.Entities;
using GaEpd.AppLibrary.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace GaEpd.AppLibrary.Domain.Repositories.EFRepository;

public abstract partial class BaseRepositoryWithMapping<TEntity, TKey, TContext>
    where TEntity : class, IEntity<TKey>
    where TKey : IEquatable<TKey>
    where TContext : DbContext
{
    // GetListAsync
    public Task<IReadOnlyCollection<TDestination>> GetListAsync<TDestination>(IMapper mapper,
        CancellationToken token = default) =>
        GetListInternal<TDestination>(mapper, predicate: null, ordering: null, token);

    public Task<IReadOnlyCollection<TDestination>> GetListAsync<TDestination>(string ordering, IMapper mapper,
        CancellationToken token = default) =>
        GetListInternal<TDestination>(mapper, predicate: null, ordering, token);

    public Task<IReadOnlyCollection<TDestination>> GetListAsync<TDestination>(Expression<Func<TEntity, bool>> predicate,
        IMapper mapper, CancellationToken token = default) =>
        GetListInternal<TDestination>(mapper, predicate, ordering: null, token);

    public Task<IReadOnlyCollection<TDestination>> GetListAsync<TDestination>(Expression<Func<TEntity, bool>> predicate,
        string ordering, IMapper mapper, CancellationToken token = default) =>
        GetListInternal<TDestination>(mapper, predicate, ordering, token);

    // Internal methods
    private async Task<IReadOnlyCollection<TDestination>> GetListInternal<TDestination>(IMapper mapper,
        Expression<Func<TEntity, bool>>? predicate, string? ordering, CancellationToken token) =>
        await mapper.ProjectTo<TDestination>(source: NoTrackingSet()
            .WhereIf(predicate)
            .OrderByIf(ordering)
        ).ToListAsync(token).ConfigureAwait(false);
}
