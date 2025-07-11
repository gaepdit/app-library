using GaEpd.AppLibrary.Domain.Entities;
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
}
