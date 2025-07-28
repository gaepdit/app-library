using AutoMapper;
using GaEpd.AppLibrary.Domain.Entities;
using GaEpd.AppLibrary.Extensions;
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

    // GetListAsync
    public Task<IReadOnlyCollection<TEntity>> GetListAsync(CancellationToken token = default) =>
        GetListInternal();

    public Task<IReadOnlyCollection<TEntity>> GetListAsync(string ordering, CancellationToken token = default) =>
        GetListInternal(ordering: ordering);

    public Task<IReadOnlyCollection<TEntity>> GetListAsync(string[] includeProperties,
        CancellationToken token = default) =>
        GetListInternal();

    public Task<IReadOnlyCollection<TEntity>> GetListAsync(string ordering, string[] includeProperties,
        CancellationToken token = default) =>
        GetListInternal(ordering: ordering);

    public Task<IReadOnlyCollection<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predicate,
        CancellationToken token = default) =>
        GetListInternal(predicate);

    public Task<IReadOnlyCollection<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predicate,
        string ordering, CancellationToken token = default) =>
        GetListInternal(predicate, ordering);

    public Task<IReadOnlyCollection<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predicate,
        string[] includeProperties, CancellationToken token = default) =>
        GetListInternal(predicate);

    public Task<IReadOnlyCollection<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predicate, string ordering,
        string[] includeProperties, CancellationToken token = default) =>
        GetListInternal(predicate, ordering);

    public Task<IReadOnlyCollection<TDestination>> GetListAsync<TDestination>(IMapper mapper,
        CancellationToken token = default) =>
        GetListInternal<TDestination>(mapper);

    public Task<IReadOnlyCollection<TDestination>> GetListAsync<TDestination>(string ordering, IMapper mapper,
        CancellationToken token = default) =>
        GetListInternal<TDestination>(mapper, ordering: ordering);

    public Task<IReadOnlyCollection<TDestination>> GetListAsync<TDestination>(
        Expression<Func<TEntity, bool>> predicate, IMapper mapper, CancellationToken token = default) =>
        GetListInternal<TDestination>(mapper, predicate);

    public Task<IReadOnlyCollection<TDestination>> GetListAsync<TDestination>(
        Expression<Func<TEntity, bool>> predicate, string ordering, IMapper mapper,
        CancellationToken token = default) =>
        GetListInternal<TDestination>(mapper, predicate, ordering);

    // Internal methods
    private async Task<IReadOnlyCollection<TEntity>> GetListInternal(Expression<Func<TEntity, bool>>? predicate = null,
        string? ordering = null) =>
        await Task.FromResult<IReadOnlyCollection<TEntity>>(
            Items.WhereIf(predicate).OrderByIf(ordering).ToList()).ConfigureAwait(false);

    private async Task<IReadOnlyCollection<TDestination>> GetListInternal<TDestination>(IMapper mapper,
        Expression<Func<TEntity, bool>>? predicate = null, string? ordering = null) =>
        await Task.FromResult(mapper.Map<IReadOnlyCollection<TDestination>>(
            Items.WhereIf(predicate).OrderByIf(ordering).ToList())).ConfigureAwait(false);
}
