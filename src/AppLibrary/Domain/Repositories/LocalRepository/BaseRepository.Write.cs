using GaEpd.AppLibrary.Domain.Entities;

namespace GaEpd.AppLibrary.Domain.Repositories.LocalRepository;

public abstract partial class BaseRepository<TEntity, TKey>
    where TEntity : IEntity<TKey>
    where TKey : IEquatable<TKey>
{
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
}
