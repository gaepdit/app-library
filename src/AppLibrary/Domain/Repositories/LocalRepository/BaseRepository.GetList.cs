using GaEpd.AppLibrary.Domain.Entities;
using GaEpd.AppLibrary.Extensions;
using System.Linq.Expressions;

namespace GaEpd.AppLibrary.Domain.Repositories.LocalRepository;

public abstract partial class BaseRepository<TEntity, TKey>
    where TEntity : IEntity<TKey>
    where TKey : IEquatable<TKey>
{
    // GetListAsync
    public Task<IReadOnlyCollection<TEntity>> GetListAsync(CancellationToken token = default) =>
        GetListInternal(predicate: null, ordering: null);

    public Task<IReadOnlyCollection<TEntity>> GetListAsync(string ordering, CancellationToken token = default) =>
        GetListInternal(predicate: null, ordering);

    public Task<IReadOnlyCollection<TEntity>> GetListAsync(string[] includeProperties,
        CancellationToken token = default) =>
        GetListInternal(predicate: null, ordering: null);

    public Task<IReadOnlyCollection<TEntity>> GetListAsync(string ordering, string[] includeProperties,
        CancellationToken token = default) =>
        GetListInternal(predicate: null, ordering);

    public Task<IReadOnlyCollection<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predicate,
        CancellationToken token = default) =>
        GetListInternal(predicate, ordering: null);

    public Task<IReadOnlyCollection<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predicate,
        string ordering, CancellationToken token = default) =>
        GetListInternal(predicate, ordering);

    public Task<IReadOnlyCollection<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predicate,
        string[] includeProperties, CancellationToken token = default) =>
        GetListInternal(predicate, ordering: null);

    public Task<IReadOnlyCollection<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predicate, string ordering,
        string[] includeProperties, CancellationToken token = default) =>
        GetListInternal(predicate, ordering);

    // Internal methods
    private async Task<IReadOnlyCollection<TEntity>> GetListInternal(Expression<Func<TEntity, bool>>? predicate,
        string? ordering) =>
        await Task.FromResult<IReadOnlyCollection<TEntity>>(Items
            .WhereIf(predicate)
            .OrderByIf(ordering)
            .ToList()).ConfigureAwait(false);
}
