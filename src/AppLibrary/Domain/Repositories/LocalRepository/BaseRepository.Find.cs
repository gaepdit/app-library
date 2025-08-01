using GaEpd.AppLibrary.Domain.Entities;
using System.Linq.Expressions;

namespace GaEpd.AppLibrary.Domain.Repositories.LocalRepository;

public abstract partial class BaseRepository<TEntity, TKey>
    where TEntity : IEntity<TKey>
    where TKey : IEquatable<TKey>
{
    // FindAsync
    public Task<TEntity?> FindAsync(TKey id, CancellationToken token = default) => FindInternal(id);

    public Task<TEntity?> FindAsync(TKey id, string[] includeProperties, CancellationToken token = default) =>
        FindInternal(id);

    public Task<TEntity?> FindAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken token = default) =>
        FindInternal(predicate);

    public Task<TEntity?> FindAsync(Expression<Func<TEntity, bool>> predicate, string[] includeProperties,
        CancellationToken token = default) =>
        FindInternal(predicate);

    // Internal methods
    private Task<TEntity?> FindInternal(TKey id) => FindInternal(entity => entity.Id.Equals(id));

    private async Task<TEntity?> FindInternal(Expression<Func<TEntity, bool>> predicate) =>
        await Task.FromResult(Items.SingleOrDefault(predicate.Compile())).ConfigureAwait(false);
}
