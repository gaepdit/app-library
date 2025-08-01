using GaEpd.AppLibrary.Domain.Entities;
using GaEpd.AppLibrary.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace GaEpd.AppLibrary.Domain.Repositories.EFRepository;

public abstract partial class BaseRepository<TEntity, TKey, TContext>
    where TEntity : class, IEntity<TKey>
    where TKey : IEquatable<TKey>
    where TContext : DbContext
{
    // GetListAsync
    public Task<IReadOnlyCollection<TEntity>> GetListAsync(CancellationToken token = default) =>
        GetListInternal(predicate: null, ordering: null, includeProperties: null, token);

    public Task<IReadOnlyCollection<TEntity>> GetListAsync(string ordering, CancellationToken token = default) =>
        GetListInternal(predicate: null, ordering, includeProperties: null, token);

    public Task<IReadOnlyCollection<TEntity>> GetListAsync(string[] includeProperties,
        CancellationToken token = default) =>
        GetListInternal(predicate: null, ordering: null, includeProperties, token);

    public Task<IReadOnlyCollection<TEntity>> GetListAsync(string ordering, string[] includeProperties,
        CancellationToken token = default) =>
        GetListInternal(predicate: null, ordering, includeProperties, token);

    public Task<IReadOnlyCollection<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predicate,
        CancellationToken token = default) =>
        GetListInternal(predicate, ordering: null, includeProperties: null, token);

    public Task<IReadOnlyCollection<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predicate, string ordering,
        CancellationToken token = default) =>
        GetListInternal(predicate, ordering, includeProperties: null, token);

    public Task<IReadOnlyCollection<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predicate,
        string[] includeProperties, CancellationToken token = default) =>
        GetListInternal(predicate, ordering: null, includeProperties, token);

    public Task<IReadOnlyCollection<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predicate,
        string ordering, string[] includeProperties, CancellationToken token = default) =>
        GetListInternal(predicate, ordering, includeProperties, token);

    // Internal methods
    private async Task<IReadOnlyCollection<TEntity>> GetListInternal(Expression<Func<TEntity, bool>>? predicate,
        string? ordering, string[]? includeProperties, CancellationToken token) =>
        await NoTrackingSet(includeProperties)
            .WhereIf(predicate)
            .OrderByIf(ordering)
            .ToListAsync(token).ConfigureAwait(false);
}
