using GaEpd.AppLibrary.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace GaEpd.AppLibrary.Domain.Repositories.EFRepository;

/// <summary>
/// An implementation of <see cref="INamedEntityRepository{TEntity}"/> using Entity Framework. The implementation is
/// derived from <see cref="BaseRepository{TEntity,TContext}"/> and uses a <see cref="Guid"/> for the primary key.
/// </summary>
/// <typeparam name="TEntity">The entity type.</typeparam>
/// <typeparam name="TContext">The type of the <see cref="DbContext"/>.</typeparam>
public abstract class NamedEntityRepository<TEntity, TContext>(TContext context)
    : BaseRepository<TEntity, TContext>(context), INamedEntityRepository<TEntity>
    where TEntity : class, IEntity, INamedEntity
    where TContext : DbContext
{
    public async Task<TEntity?> FindByNameAsync(string name, CancellationToken token = default) =>
        await Context.Set<TEntity>().AsNoTracking()
            .SingleOrDefaultAsync(entity => string.Equals(entity.Name.ToUpper(), name.ToUpper()), token)
            .ConfigureAwait(false);

    public async Task<IReadOnlyCollection<TEntity>> GetOrderedListAsync(CancellationToken token = default) =>
        await Context.Set<TEntity>().AsNoTracking()
            .OrderBy(entity => entity.Name).ThenBy(entity => entity.Id).ToListAsync(token).ConfigureAwait(false);

    public async Task<IReadOnlyCollection<TEntity>> GetOrderedListAsync(Expression<Func<TEntity, bool>> predicate,
        CancellationToken token = default) =>
        await Context.Set<TEntity>().AsNoTracking().Where(predicate)
            .OrderBy(entity => entity.Name).ThenBy(entity => entity.Id).ToListAsync(token).ConfigureAwait(false);
}
