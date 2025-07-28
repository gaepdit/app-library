using GaEpd.AppLibrary.Domain.Entities;

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
public abstract partial class BaseRepository<TEntity, TKey>(IEnumerable<TEntity> items)
    : IRepository<TEntity, TKey>
    where TEntity : IEntity<TKey>
    where TKey : IEquatable<TKey>
{
    public ICollection<TEntity> Items { get; } = items.ToList();

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
