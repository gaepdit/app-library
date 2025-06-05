using GaEpd.AppLibrary.Domain.Entities;
using GaEpd.AppLibrary.Pagination;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;

namespace GaEpd.AppLibrary.Domain.Repositories.EFRepository;

/// <summary>
/// An implementation of <see cref="IRepository{TEntity,TKey}"/> using Entity Framework where TKey is
/// a <see cref="Guid"/> primary key.
/// </summary>
/// <typeparam name="TEntity">The entity type.</typeparam>
/// <typeparam name="TContext">The type of the <see cref="DbContext"/>.</typeparam>
public abstract class BaseRepository<TEntity, TContext>(TContext context)
    : BaseRepository<TEntity, Guid, DbContext>(context), IRepository<TEntity>
    where TEntity : class, IEntity
    where TContext : DbContext;

/// <summary>
/// An implementation of <see cref="IRepository{TEntity,TKey}"/> using Entity Framework.
/// </summary>
/// <typeparam name="TEntity">The entity type.</typeparam>
/// <typeparam name="TKey">The primary key type for the entity.</typeparam>
/// <typeparam name="TContext">The type of the <see cref="DbContext"/>.</typeparam>
[SuppressMessage("", "S2436")]
public abstract class BaseRepository<TEntity, TKey, TContext>(TContext context)
    : IRepository<TEntity, TKey>
    where TEntity : class, IEntity<TKey>
    where TKey : IEquatable<TKey>
    where TContext : DbContext
{
    public TContext Context => context;

    // GetAsync
    public Task<TEntity> GetAsync(TKey id, CancellationToken token = default) =>
        GetAsyncInternal(id, token: token);

    public Task<TEntity> GetAsync(TKey id, string[] includeProperties, CancellationToken token = default) =>
        GetAsyncInternal(id, includeProperties, token);

    private async Task<TEntity> GetAsyncInternal(TKey id, string[]? includeProperties = null,
        CancellationToken token = default) =>
        await TrackingSet(includeProperties).SingleOrDefaultAsync(entity => entity.Id.Equals(id), token)
            .ConfigureAwait(false) ?? throw new EntityNotFoundException<TEntity>(id);

    // FindAsync
    public Task<TEntity?> FindAsync(TKey id, CancellationToken token = default) =>
        FindAsyncInternal(id, token: token);

    public Task<TEntity?> FindAsync(TKey id, string[] includeProperties, CancellationToken token = default) =>
        FindAsyncInternal(id, includeProperties, token);

    public Task<TEntity?> FindAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken token = default) =>
        FindAsyncInternal(predicate, token: token);

    public Task<TEntity?> FindAsync(Expression<Func<TEntity, bool>> predicate, string[] includeProperties,
        CancellationToken token = default) =>
        FindAsyncInternal(predicate, includeProperties, token);

    private Task<TEntity?> FindAsyncInternal(TKey id, string[]? includeProperties = null,
        CancellationToken token = default) =>
        FindAsyncInternal(entity => entity.Id.Equals(id), includeProperties, token);

    private async Task<TEntity?> FindAsyncInternal(Expression<Func<TEntity, bool>> predicate,
        string[]? includeProperties = null, CancellationToken token = default) =>
        await NoTrackingSet(includeProperties).SingleOrDefaultAsync(predicate, token).ConfigureAwait(false);

    // GetListAsync
    public Task<IReadOnlyCollection<TEntity>> GetListAsync(CancellationToken token = default) =>
        GetListAsyncInternal(token: token);

    public Task<IReadOnlyCollection<TEntity>> GetListAsync(string ordering, CancellationToken token = default) =>
        GetListAsyncInternal(ordering, token: token);

    public Task<IReadOnlyCollection<TEntity>> GetListAsync(string[] includeProperties,
        CancellationToken token = default) =>
        GetListAsyncInternal(includeProperties: includeProperties, token: token);

    public Task<IReadOnlyCollection<TEntity>> GetListAsync(string ordering, string[] includeProperties,
        CancellationToken token = default) =>
        GetListAsyncInternal(ordering, includeProperties, token);

    public Task<IReadOnlyCollection<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predicate,
        CancellationToken token = default) =>
        GetListAsyncInternal(predicate, token: token);

    public Task<IReadOnlyCollection<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predicate, string ordering,
        CancellationToken token = default) =>
        GetListAsyncInternal(predicate, ordering, token: token);

    public Task<IReadOnlyCollection<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predicate,
        string[] includeProperties, CancellationToken token = default) =>
        GetListAsyncInternal(predicate, includeProperties: includeProperties, token: token);

    public Task<IReadOnlyCollection<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predicate,
        string ordering, string[] includeProperties, CancellationToken token = default) =>
        GetListAsyncInternal(predicate, ordering, includeProperties, token);

    private async Task<IReadOnlyCollection<TEntity>> GetListAsyncInternal(string? ordering = null,
        string[]? includeProperties = null, CancellationToken token = default) =>
        await NoTrackingSet(includeProperties).OrderByIf(ordering).ToListAsync(token).ConfigureAwait(false);

    private async Task<IReadOnlyCollection<TEntity>> GetListAsyncInternal(Expression<Func<TEntity, bool>> predicate,
        string? ordering = null, string[]? includeProperties = null, CancellationToken token = default) =>
        await NoTrackingSet(includeProperties).Where(predicate).OrderByIf(ordering).ToListAsync(token)
            .ConfigureAwait(false);

    // GetPagedListAsync
    public Task<IReadOnlyCollection<TEntity>> GetPagedListAsync(Expression<Func<TEntity, bool>> predicate,
        PaginatedRequest paging, CancellationToken token = default) =>
        GetPagedListAsyncInternal(predicate, paging, token: token);

    public Task<IReadOnlyCollection<TEntity>> GetPagedListAsync(Expression<Func<TEntity, bool>> predicate,
        PaginatedRequest paging, string[] includeProperties, CancellationToken token = default) =>
        GetPagedListAsyncInternal(predicate, paging, includeProperties, token);

    public Task<IReadOnlyCollection<TEntity>> GetPagedListAsync(PaginatedRequest paging,
        CancellationToken token = default) =>
        GetPagedListAsyncInternal(paging, token: token);

    public Task<IReadOnlyCollection<TEntity>> GetPagedListAsync(PaginatedRequest paging, string[] includeProperties,
        CancellationToken token = default) =>
        GetPagedListAsyncInternal(paging, includeProperties, token);

    private async Task<IReadOnlyCollection<TEntity>> GetPagedListAsyncInternal(
        Expression<Func<TEntity, bool>> predicate, PaginatedRequest paging, string[]? includeProperties = null,
        CancellationToken token = default) =>
        await ApplyPagingAsync(NoTrackingSet(includeProperties).Where(predicate), paging, token).ConfigureAwait(false);

    private async Task<IReadOnlyCollection<TEntity>> GetPagedListAsyncInternal(PaginatedRequest paging,
        string[]? includeProperties = null, CancellationToken token = default) =>
        await ApplyPagingAsync(NoTrackingSet(includeProperties), paging, token).ConfigureAwait(false);

    private static Task<List<TEntity>> ApplyPagingAsync(IQueryable<TEntity> queryable, PaginatedRequest paging,
        CancellationToken token) =>
        queryable.OrderByIf(paging.Sorting).Skip(paging.Skip).Take(paging.Take).ToListAsync(token);

    // CountAsync
    public async Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken token = default) =>
        await NoTrackingSet().CountAsync(predicate, token).ConfigureAwait(false);

    // ExistsAsync
    public Task<bool> ExistsAsync(TKey id, CancellationToken token = default) =>
        ExistsAsync(entity => entity.Id.Equals(id), token);

    public async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken token = default) =>
        await NoTrackingSet().AnyAsync(predicate, token).ConfigureAwait(false);

    // Common IReadRepository methods
    private IQueryable<TEntity> TrackingSet(string[]? includeProperties) =>
        includeProperties is null || includeProperties.Length == 0
            ? Context.Set<TEntity>()
            : includeProperties.Aggregate(Context.Set<TEntity>().AsQueryable(),
                (queryable, includeProperty) => queryable.Include(includeProperty));

    private IQueryable<TEntity> NoTrackingSet(string[]? includeProperties = null) =>
        TrackingSet(includeProperties).AsNoTracking();

    // IWriteRepository methods
    public async Task InsertAsync(TEntity entity, bool autoSave = true, CancellationToken token = default)
    {
        await Context.Set<TEntity>().AddAsync(entity, token).ConfigureAwait(false);
        if (autoSave) await SaveChangesAsync(token).ConfigureAwait(false);
    }

    public async Task UpdateAsync(TEntity entity, bool autoSave = true, CancellationToken token = default)
    {
        Context.Attach(entity);
        Context.Update(entity);

        if (!autoSave) return;

        try
        {
            await SaveChangesAsync(token).ConfigureAwait(false);
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await Context.Set<TEntity>().AsNoTracking()
                    .AnyAsync(e => e.Id.Equals(entity.Id), token).ConfigureAwait(false))
                throw new EntityNotFoundException<TEntity>(entity.Id);
            throw;
        }
    }

    public async Task DeleteAsync(TEntity entity, bool autoSave = true, CancellationToken token = default)
    {
        Context.Set<TEntity>().Remove(entity);

        try
        {
            if (autoSave) await SaveChangesAsync(token).ConfigureAwait(false);
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await Context.Set<TEntity>().AsNoTracking()
                    .AnyAsync(e => e.Id.Equals(entity.Id), token).ConfigureAwait(false))
                throw new EntityNotFoundException<TEntity>(entity.Id);
            throw;
        }
    }

    public async Task SaveChangesAsync(CancellationToken token = default) =>
        await Context.SaveChangesAsync(token).ConfigureAwait(false);

    #region IDisposable, IAsyncDisposable

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
        if (_disposed) return;
        Context.Dispose();
        _disposed = true;
    }

    // ReSharper disable once VirtualMemberNeverOverridden.Global
    protected virtual ValueTask DisposeAsyncCore() => Context.DisposeAsync();

    #endregion
}
