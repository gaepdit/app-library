using GaEpd.AppLibrary.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace GaEpd.AppLibrary.Domain.Repositories.EFRepository;

public abstract partial class BaseRepository<TEntity, TKey, TContext>
    where TEntity : class, IEntity<TKey>
    where TKey : IEquatable<TKey>
    where TContext : DbContext
{
    // CountAsync
    public async Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken token = default) =>
        await NoTrackingSet().CountAsync(predicate, token).ConfigureAwait(false);
}
