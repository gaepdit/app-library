using GaEpd.AppLibrary.Domain.Entities;
using GaEpd.AppLibrary.Pagination;
using System.Linq.Expressions;

namespace GaEpd.AppLibrary.Domain.Repositories.LocalRepository;

/// <summary>
/// An implementation of <see cref="IRepository{TEntity,TKey}"/> using in-memory data where TKey is
/// a <see cref="Guid"/> primary key.
/// </summary>
/// <typeparam name="TEntity">The entity type.</typeparam>
public abstract class BaseRepository<TEntity>(IEnumerable<TEntity> items)
    : BaseRepository<TEntity, Guid>(items), IRepository<TEntity>
    where TEntity : class, IEntity;

/// <summary>
/// An implementation of <see cref="IRepository{TEntity,TKey}"/> using in-memory data.
/// </summary>
/// <typeparam name="TEntity">The entity type.</typeparam>
/// <typeparam name="TKey">The primary key type for the entity.</typeparam>
/// <remarks>Navigation properties are already included when using in-memory data structures,
/// so any `includeProperties` parameters are ignored.</remarks>
public abstract class BaseRepository<TEntity, TKey>(IEnumerable<TEntity> items)
    : IRepository<TEntity, TKey>
    where TEntity : IEntity<TKey>
    where TKey : IEquatable<TKey>
{
    public ICollection<TEntity> Items { get; } = items.ToList();

    // Navigation properties are already included when using in-memory data structures so are not used in the internal methods.

    // GetAsync
    public Task<TEntity> GetAsync(TKey id, CancellationToken token = default) =>
        GetAsyncInternal(id);

    public Task<TEntity> GetAsync(TKey id, string[] includeProperties, CancellationToken token = default) =>
        GetAsyncInternal(id);

    private async Task<TEntity> GetAsyncInternal(TKey id) =>
        await FindAsyncInternal(id).ConfigureAwait(false) ??
        throw new EntityNotFoundException<TEntity>(id);

    // FindAsync
    public Task<TEntity?> FindAsync(TKey id, CancellationToken token = default) =>
        FindAsyncInternal(id);

    public Task<TEntity?> FindAsync(TKey id, string[] includeProperties, CancellationToken token = default) =>
        FindAsyncInternal(id);

    public Task<TEntity?> FindAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken token = default) =>
        FindAsyncInternal(predicate);

    public Task<TEntity?> FindAsync(Expression<Func<TEntity, bool>> predicate, string[] includeProperties,
        CancellationToken token = default) =>
        FindAsyncInternal(predicate);

    private Task<TEntity?> FindAsyncInternal(TKey id) =>
        FindAsyncInternal(entity => entity.Id.Equals(id));

    private async Task<TEntity?> FindAsyncInternal(Expression<Func<TEntity, bool>> predicate) =>
        await Task.FromResult(Items.SingleOrDefault(predicate.Compile())).ConfigureAwait(false);

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

    // GetPagedListAsync
    public Task<IReadOnlyCollection<TEntity>> GetPagedListAsync(Expression<Func<TEntity, bool>> predicate,
        PaginatedRequest paging, CancellationToken token = default) =>
        GetPagedListAsyncInternal(predicate, paging);

    public Task<IReadOnlyCollection<TEntity>> GetPagedListAsync(Expression<Func<TEntity, bool>> predicate,
        PaginatedRequest paging, string[] includeProperties, CancellationToken token = default) =>
        GetPagedListAsyncInternal(predicate, paging);

    public Task<IReadOnlyCollection<TEntity>> GetPagedListAsync(PaginatedRequest paging,
        CancellationToken token = default) =>
        GetPagedListAsyncInternal(paging);

    public Task<IReadOnlyCollection<TEntity>> GetPagedListAsync(PaginatedRequest paging,
        string[] includeProperties, CancellationToken token = default) =>
        GetPagedListAsyncInternal(paging);

    private async Task<IReadOnlyCollection<TEntity>> GetPagedListAsyncInternal(
        Expression<Func<TEntity, bool>> predicate, PaginatedRequest paging) =>
        await Task.FromResult<IReadOnlyCollection<TEntity>>(Items.Where(predicate.Compile()).AsQueryable()
            .OrderByIf(paging.Sorting).Skip(paging.Skip).Take(paging.Take).ToList()).ConfigureAwait(false);

    private async Task<IReadOnlyCollection<TEntity>> GetPagedListAsyncInternal(PaginatedRequest paging) =>
        await Task.FromResult<IReadOnlyCollection<TEntity>>(Items.AsQueryable()
            .OrderByIf(paging.Sorting).Skip(paging.Skip).Take(paging.Take).ToList()).ConfigureAwait(false);

    // CountAsync
    public async Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken token = default) =>
        await Task.FromResult(Items.Count(predicate.Compile())).ConfigureAwait(false);

    // ExistsAsync
    public Task<bool> ExistsAsync(TKey id, CancellationToken token = default) =>
        ExistsAsync(entity => entity.Id.Equals(id), token);

    public async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken token = default) =>
        await Task.FromResult(Items.Any(predicate.Compile())).ConfigureAwait(false);

    // IWriteRepository methods
    public Task InsertAsync(TEntity entity, bool autoSave = true, CancellationToken token = default)
    {
        Items.Add(entity);
        return Task.CompletedTask;
    }

    public async Task UpdateAsync(TEntity entity, bool autoSave = true, CancellationToken token = default)
    {
        var item = await GetAsync(entity.Id, token: token).ConfigureAwait(false);
        Items.Remove(item);
        Items.Add(entity);
    }

    public async Task DeleteAsync(TEntity entity, bool autoSave = true, CancellationToken token = default) =>
        Items.Remove(await GetAsync(entity.Id, token: token).ConfigureAwait(false));

    // Local repository does not require changes to be explicitly saved.
    public Task SaveChangesAsync(CancellationToken token = default) => Task.CompletedTask;

    #region IDisposable,  IAsyncDisposable

    private bool _disposed;
    ~BaseRepository() => Dispose(disposing: false);

    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(obj: this);
    }

    public async ValueTask DisposeAsync()
    {
        await DisposeAsyncCore().ConfigureAwait(false);
        Dispose(disposing: false);
        GC.SuppressFinalize(obj: this);
    }

    // ReSharper disable once VirtualMemberNeverOverridden.Global
    // ReSharper disable once UnusedParameter.Global
    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed) _disposed = true;
    }

    // ReSharper disable once VirtualMemberNeverOverridden.Global
    protected virtual ValueTask DisposeAsyncCore() => ValueTask.CompletedTask;

    #endregion
}
