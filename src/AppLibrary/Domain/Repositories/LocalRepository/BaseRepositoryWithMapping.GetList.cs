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
        GetListInternal<TDestination>(mapper, predicate: null, ordering: null);

    public Task<IReadOnlyCollection<TDestination>> GetListAsync<TDestination>(string ordering, IMapper mapper,
        CancellationToken token = default) =>
        GetListInternal<TDestination>(mapper, predicate: null, ordering);

    public Task<IReadOnlyCollection<TDestination>> GetListAsync<TDestination>(
        Expression<Func<TEntity, bool>> predicate, IMapper mapper, CancellationToken token = default) =>
        GetListInternal<TDestination>(mapper, predicate, ordering: null);

    public Task<IReadOnlyCollection<TDestination>> GetListAsync<TDestination>(
        Expression<Func<TEntity, bool>> predicate, string ordering, IMapper mapper,
        CancellationToken token = default) =>
        GetListInternal<TDestination>(mapper, predicate, ordering);

    public Task<IReadOnlyCollection<TDestination>> GetListAsync<TDestination, TSource>(IMapper mapper,
        CancellationToken token = default) where TSource : TEntity =>
        GetListInternal<TDestination, TSource>(mapper, predicate: null, ordering: null);

    public Task<IReadOnlyCollection<TDestination>> GetListAsync<TDestination, TSource>(string ordering,
        IMapper mapper, CancellationToken token = default) where TSource : TEntity =>
        GetListInternal<TDestination, TSource>(mapper, predicate: null, ordering);

    public Task<IReadOnlyCollection<TDestination>> GetListAsync<TDestination, TSource>(
        Expression<Func<TSource, bool>> predicate, IMapper mapper, CancellationToken token = default)
        where TSource : TEntity =>
        GetListInternal<TDestination, TSource>(mapper, predicate, ordering: null);

    public Task<IReadOnlyCollection<TDestination>> GetListAsync<TDestination, TSource>(
        Expression<Func<TSource, bool>> predicate, string ordering, IMapper mapper,
        CancellationToken token = default) where TSource : TEntity =>
        GetListInternal<TDestination, TSource>(mapper, predicate, ordering);

    // Internal methods
    private async Task<IReadOnlyCollection<TDestination>> GetListInternal<TDestination>(IMapper mapper,
        Expression<Func<TEntity, bool>>? predicate, string? ordering) =>
        await Task.FromResult(mapper.Map<IReadOnlyCollection<TDestination>>(Items
            .WhereIf(predicate)
            .OrderByIf(ordering)
            .ToList())).ConfigureAwait(false);

    private async Task<IReadOnlyCollection<TDestination>> GetListInternal<TDestination, TSource>(IMapper mapper,
        Expression<Func<TSource, bool>>? predicate, string? ordering) where TSource : TEntity =>
        await Task.FromResult(mapper.Map<IReadOnlyCollection<TDestination>>(Items
            .OfType<TSource>()
            .WhereIf(predicate)
            .OrderByIf(ordering)
            .ToList())).ConfigureAwait(false);
}
