using AutoMapper;
using GaEpd.AppLibrary.Domain.Entities;
using GaEpd.AppLibrary.Extensions;
using GaEpd.AppLibrary.Pagination;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;

namespace GaEpd.AppLibrary.Domain.Repositories.LocalRepository;

public abstract partial class BaseRepositoryWithMapping<TEntity, TKey>
    where TEntity : IEntity<TKey>
    where TKey : IEquatable<TKey>
{
    // GetPagedListAsync
    public Task<IReadOnlyCollection<TDestination>> GetPagedListAsync<TDestination>(PaginatedRequest paging,
        IMapper mapper, CancellationToken token = default) =>
        GetPagedListInternal<TDestination>(mapper, paging, predicate: null);

    public Task<IReadOnlyCollection<TDestination>> GetPagedListAsync<TDestination>(
        Expression<Func<TEntity, bool>> predicate, PaginatedRequest paging, IMapper mapper,
        CancellationToken token = default) =>
        GetPagedListInternal<TDestination>(mapper, paging, predicate);

    // Internal methods
    private async Task<IReadOnlyCollection<TDestination>> GetPagedListInternal<TDestination>(IMapper mapper,
        PaginatedRequest paging, Expression<Func<TEntity, bool>>? predicate) =>
        await Task.FromResult(mapper.Map<IReadOnlyCollection<TDestination>>(Items
            .WhereIf(predicate)
            .OrderBy(paging.Sorting)
            .Skip(paging.Skip).Take(paging.Take)
            .ToList())).ConfigureAwait(false);
}
