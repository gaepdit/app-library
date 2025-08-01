using GaEpd.AppLibrary.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

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
public abstract partial class BaseRepository<TEntity, TKey, TContext>(TContext context)
    : IRepository<TEntity, TKey>
    where TEntity : class, IEntity<TKey>
    where TKey : IEquatable<TKey>
    where TContext : DbContext
{
    public TContext Context => context;

    public async Task SaveChangesAsync(CancellationToken token = default) =>
        await Context.SaveChangesAsync(token).ConfigureAwait(false);

    // Common IReadRepository methods
    private static IQueryable<TEntity> SetWithIncludes(IQueryable<TEntity> entities, string[]? includes) =>
        includes is null || includes.Length == 0
            ? entities
            : includes.Aggregate(entities, (queryable, property) => queryable.Include(property));

    protected IQueryable<TEntity> NoTrackingSet() =>
        Context.ChangeTracker.QueryTrackingBehavior == QueryTrackingBehavior.NoTrackingWithIdentityResolution
            ? Context.Set<TEntity>().AsNoTrackingWithIdentityResolution()
            : Context.Set<TEntity>().AsNoTracking();

    private IQueryable<TEntity> NoTrackingSet(string[]? includeProperties) =>
        SetWithIncludes(NoTrackingSet(), includeProperties);

    private IQueryable<TEntity> TrackingSet(string[]? includeProperties) =>
        SetWithIncludes(Context.Set<TEntity>().AsTracking(), includeProperties);

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
