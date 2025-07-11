using GaEpd.AppLibrary.Domain.Entities;
using GaEpd.AppLibrary.Pagination;
using System.Linq.Expressions;

namespace GaEpd.AppLibrary.Domain.Repositories.LocalRepository;

/// <summary>
/// An implementation of <see cref="IRepository{TEntity,TKey}"/> using in-memory data.
/// </summary>
/// <typeparam name="TEntity">The entity type.</typeparam>
/// <typeparam name="TKey">The primary key type for the entity.</typeparam>
/// <remarks>Navigation properties are already included when using in-memory data structures,
/// so any `includeProperties` parameters are ignored.</remarks>
public abstract partial class BaseRepository<TEntity, TKey>
    where TEntity : IEntity<TKey>
    where TKey : IEquatable<TKey>
{
    // Navigation properties are already included when using in-memory data structures so are not used in the internal methods.

    // GetListAsync
    public Task<IReadOnlyCollection<TEntity>> GetListAsync(CancellationToken token = default) =>
        GetListAsyncInternal();

    public Task<IReadOnlyCollection<TEntity>> GetListAsync(string ordering, CancellationToken token = default) =>
        GetListAsyncInternal(ordering);

    public Task<IReadOnlyCollection<TEntity>> GetListAsync(string[] includeProperties,
        CancellationToken token = default) =>
        GetListAsyncInternal();

    public Task<IReadOnlyCollection<TEntity>> GetListAsync(string ordering, string[] includeProperties,
        CancellationToken token = default) =>
        GetListAsyncInternal(ordering);

    public Task<IReadOnlyCollection<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predicate,
        CancellationToken token = default) =>
        GetListAsyncInternal(predicate);

    public Task<IReadOnlyCollection<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predicate,
        string ordering, CancellationToken token = default) =>
        GetListAsyncInternal(predicate, ordering);

    public Task<IReadOnlyCollection<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predicate,
        string[] includeProperties, CancellationToken token = default) =>
        GetListAsyncInternal(predicate);

    public Task<IReadOnlyCollection<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predicate, string ordering,
        string[] includeProperties, CancellationToken token = default) =>
        GetListAsyncInternal(predicate, ordering);

    private async Task<IReadOnlyCollection<TEntity>> GetListAsyncInternal(string? ordering = null) =>
        await Task.FromResult<IReadOnlyCollection<TEntity>>(Items.AsQueryable().OrderByIf(ordering).ToList())
            .ConfigureAwait(false);

    private async Task<IReadOnlyCollection<TEntity>> GetListAsyncInternal(Expression<Func<TEntity, bool>> predicate,
        string? ordering = null) =>
        await Task.FromResult<IReadOnlyCollection<TEntity>>(Items.Where(predicate.Compile()).AsQueryable()
            .OrderByIf(ordering).ToList()).ConfigureAwait(false);
}
