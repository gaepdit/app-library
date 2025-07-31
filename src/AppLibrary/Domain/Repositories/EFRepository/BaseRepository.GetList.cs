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
public abstract partial class BaseRepository<TEntity, TKey, TContext>
    where TEntity : class, IEntity<TKey>
    where TKey : IEquatable<TKey>
    where TContext : DbContext
{
    // GetListAsync
    public Task<IReadOnlyCollection<TEntity>> GetListAsync(CancellationToken token = default) =>
        GetListInternal(token: token);

    public Task<IReadOnlyCollection<TEntity>> GetListAsync(string ordering, CancellationToken token = default) =>
        GetListInternal(ordering: ordering, token: token);

    public Task<IReadOnlyCollection<TEntity>> GetListAsync(string[] includeProperties,
        CancellationToken token = default) =>
        GetListInternal(includeProperties: includeProperties, token: token);

    public Task<IReadOnlyCollection<TEntity>> GetListAsync(string ordering, string[] includeProperties,
        CancellationToken token = default) =>
        GetListInternal(ordering: ordering, includeProperties: includeProperties, token: token);

    public Task<IReadOnlyCollection<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predicate,
        CancellationToken token = default) =>
        GetListInternal(predicate, token: token);

    public Task<IReadOnlyCollection<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predicate, string ordering,
        CancellationToken token = default) =>
        GetListInternal(predicate, ordering, token: token);

    public Task<IReadOnlyCollection<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predicate,
        string[] includeProperties, CancellationToken token = default) =>
        GetListInternal(predicate, includeProperties: includeProperties, token: token);

    public Task<IReadOnlyCollection<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predicate,
        string ordering, string[] includeProperties, CancellationToken token = default) =>
        GetListInternal(predicate, ordering, includeProperties, token);

    // Internal methods
    private async Task<IReadOnlyCollection<TEntity>> GetListInternal(
        Expression<Func<TEntity, bool>>? predicate = null, string? ordering = null,
        string[]? includeProperties = null, CancellationToken token = default) =>
        await NoTrackingSet(includeProperties)
            .WhereIf(predicate)
            .OrderByIf(ordering)
            .ToListAsync(token).ConfigureAwait(false);
}
