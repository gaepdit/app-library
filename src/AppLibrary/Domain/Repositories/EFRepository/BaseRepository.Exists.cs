using GaEpd.AppLibrary.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace GaEpd.AppLibrary.Domain.Repositories.EFRepository;

public abstract partial class BaseRepository<TEntity, TKey, TContext>
    where TEntity : class, IEntity<TKey>
    where TKey : IEquatable<TKey>
    where TContext : DbContext
{
    // ExistsAsync
    public Task<bool> ExistsAsync(TKey id, CancellationToken token = default) =>
        ExistsAsync(entity => entity.Id.Equals(id), token);

    public async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken token = default) =>
        await NoTrackingSet().AnyAsync(predicate, token).ConfigureAwait(false);
}
