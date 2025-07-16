using AutoMapper;
using AutoMapper.QueryableExtensions;
using GaEpd.AppLibrary.Domain.Entities;
using GaEpd.AppLibrary.Extensions;
using GaEpd.AppLibrary.Pagination;
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
    // GetPagedListAsync
    public Task<IReadOnlyCollection<TEntity>> GetPagedListAsync(Expression<Func<TEntity, bool>> predicate,
        PaginatedRequest paging, CancellationToken token = default) =>
        GetPagedListInternal(paging, predicate, token: token);

    public Task<IReadOnlyCollection<TEntity>> GetPagedListAsync(Expression<Func<TEntity, bool>> predicate,
        PaginatedRequest paging, string[] includeProperties, CancellationToken token = default) =>
        GetPagedListInternal(paging, predicate, includeProperties, token);

    public Task<IReadOnlyCollection<TEntity>> GetPagedListAsync(PaginatedRequest paging,
        CancellationToken token = default) =>
        GetPagedListInternal(paging, token: token);

    public Task<IReadOnlyCollection<TEntity>> GetPagedListAsync(PaginatedRequest paging, string[] includeProperties,
        CancellationToken token = default) =>
        GetPagedListInternal(paging, includeProperties: includeProperties, token: token);

    public Task<IReadOnlyCollection<TDestination>> GetPagedListAsync<TDestination>(
        Expression<Func<TEntity, bool>> predicate, PaginatedRequest paging, IMapper mapper,
        CancellationToken token = default) =>
        GetPagedListInternal<TDestination>(mapper, paging, predicate, token);

    public Task<IReadOnlyCollection<TDestination>> GetPagedListAsync<TDestination>(PaginatedRequest paging,
        IMapper mapper, CancellationToken token = default) =>
        GetPagedListInternal<TDestination>(mapper, paging, token: token);

    // Internal methods
    private async Task<IReadOnlyCollection<TEntity>> GetPagedListInternal(PaginatedRequest paging,
        Expression<Func<TEntity, bool>>? predicate = null, string[]? includeProperties = null,
        CancellationToken token = default) =>
        await NoTrackingSet(includeProperties)
            .WhereIf(predicate)
            .ApplyPaging(paging)
            .ToListAsync(token).ConfigureAwait(false);

    private async Task<IReadOnlyCollection<TDestination>> GetPagedListInternal<TDestination>(IMapper mapper,
        PaginatedRequest paging, Expression<Func<TEntity, bool>>? predicate = null,
        CancellationToken token = default) =>
        await Context.Set<TEntity>().AsNoTracking()
            .WhereIf(predicate)
            .ApplyPaging(paging)
            .ProjectTo<TDestination>(mapper.ConfigurationProvider)
            .ToListAsync(token).ConfigureAwait(false);
}
