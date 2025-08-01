using GaEpd.AppLibrary.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GaEpd.AppLibrary.Domain.Repositories.EFRepository;

public abstract partial class BaseRepository<TEntity, TKey, TContext>
    where TEntity : class, IEntity<TKey>
    where TKey : IEquatable<TKey>
    where TContext : DbContext
{
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
            if (!await NoTrackingSet().AnyAsync(e => e.Id.Equals(entity.Id), token).ConfigureAwait(false))
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
            if (!await NoTrackingSet().AnyAsync(e => e.Id.Equals(entity.Id), token).ConfigureAwait(false))
                throw new EntityNotFoundException<TEntity>(entity.Id);
            throw;
        }
    }
}
