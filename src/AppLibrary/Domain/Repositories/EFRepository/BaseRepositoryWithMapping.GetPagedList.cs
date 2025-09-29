using AutoMapper;
using GaEpd.AppLibrary.Domain.Entities;
using GaEpd.AppLibrary.Extensions;
using GaEpd.AppLibrary.Pagination;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace GaEpd.AppLibrary.Domain.Repositories.EFRepository;

public abstract partial class BaseRepositoryWithMapping<TEntity, TKey, TContext>
    where TEntity : class, IEntity<TKey>
    where TKey : IEquatable<TKey>
    where TContext : DbContext
{
    // GetPagedListAsync
    public Task<IReadOnlyCollection<TDestination>> GetPagedListAsync<TDestination>(PaginatedRequest paging,
        IMapper mapper, CancellationToken token = default) =>
        GetPagedListInternal<TDestination>(mapper, paging, predicate: null, token);

    public Task<IReadOnlyCollection<TDestination>> GetPagedListAsync<TDestination>(
        Expression<Func<TEntity, bool>> predicate, PaginatedRequest paging, IMapper mapper,
        CancellationToken token = default) =>
        GetPagedListInternal<TDestination>(mapper, paging, predicate, token);

    public Task<IReadOnlyCollection<TDestination>> GetPagedListAsync<TDestination, TSource>(
        PaginatedRequest paging, IMapper mapper,
        CancellationToken token = default) where TSource : TEntity =>
        GetPagedListInternal<TDestination, TSource>(mapper, paging, predicate: null, token);

    public Task<IReadOnlyCollection<TDestination>> GetPagedListAsync<TDestination, TSource>(
        Expression<Func<TSource, bool>> predicate, PaginatedRequest paging, IMapper mapper,
        CancellationToken token = default) where TSource : TEntity =>
        GetPagedListInternal<TDestination, TSource>(mapper, paging, predicate, token);

    // Internal methods
    private async Task<IReadOnlyCollection<TDestination>> GetPagedListInternal<TDestination>(IMapper mapper,
        PaginatedRequest paging, Expression<Func<TEntity, bool>>? predicate, CancellationToken token) =>
        await mapper.ProjectTo<TDestination>(source: NoTrackingSet()
            .WhereIf(predicate)
            .ApplyPaging(paging)
        ).ToListAsync(token).ConfigureAwait(false);

    private async Task<IReadOnlyCollection<TDestination>> GetPagedListInternal<TDestination, TSource>(IMapper mapper,
        PaginatedRequest paging, Expression<Func<TSource, bool>>? predicate, CancellationToken token)
        where TSource : TEntity =>
        await mapper.ProjectTo<TDestination>(source: NoTrackingSet()
            .OfType<TSource>()
            .WhereIf(predicate)
            .ApplyPaging(paging)
        ).ToListAsync(token).ConfigureAwait(false);
}
