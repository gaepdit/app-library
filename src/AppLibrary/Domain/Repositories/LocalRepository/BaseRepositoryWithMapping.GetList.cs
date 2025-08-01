using AutoMapper;
using GaEpd.AppLibrary.Domain.Entities;
using GaEpd.AppLibrary.Extensions;
using System.Linq.Expressions;

namespace GaEpd.AppLibrary.Domain.Repositories.LocalRepository;

public abstract partial class BaseRepositoryWithMapping<TEntity, TKey>
    where TEntity : IEntity<TKey>
    where TKey : IEquatable<TKey>
{
    public Task<IReadOnlyCollection<TDestination>> GetListAsync<TDestination>(IMapper mapper,
        CancellationToken token = default) =>
        GetListInternal<TDestination>(mapper);

    public Task<IReadOnlyCollection<TDestination>> GetListAsync<TDestination>(string ordering, IMapper mapper,
        CancellationToken token = default) =>
        GetListInternal<TDestination>(mapper, ordering: ordering);

    public Task<IReadOnlyCollection<TDestination>> GetListAsync<TDestination>(
        Expression<Func<TEntity, bool>> predicate, IMapper mapper, CancellationToken token = default) =>
        GetListInternal<TDestination>(mapper, predicate);

    public Task<IReadOnlyCollection<TDestination>> GetListAsync<TDestination>(
        Expression<Func<TEntity, bool>> predicate, string ordering, IMapper mapper,
        CancellationToken token = default) =>
        GetListInternal<TDestination>(mapper, predicate, ordering);

    // Internal methods
    private async Task<IReadOnlyCollection<TDestination>> GetListInternal<TDestination>(IMapper mapper,
        Expression<Func<TEntity, bool>>? predicate = null, string? ordering = null) =>
        await Task.FromResult(mapper.Map<IReadOnlyCollection<TDestination>>(
            Items.WhereIf(predicate).OrderByIf(ordering).ToList())).ConfigureAwait(false);
}
