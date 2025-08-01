using AutoMapper;
using GaEpd.AppLibrary.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace GaEpd.AppLibrary.Domain.Repositories.EFRepository;

/// <summary>
/// An implementation of <see cref="INamedEntityRepositoryWithMapping{TEntity}"/> using Entity Framework. The
/// implementation is derived from <see cref="BaseRepositoryWithMapping{TEntity,TContext}"/> and uses
/// a <see cref="Guid"/> for the primary key.
/// </summary>
/// <typeparam name="TEntity">The entity type.</typeparam>
/// <typeparam name="TContext">The type of the <see cref="DbContext"/>.</typeparam>
public abstract class NamedEntityRepositoryWithMapping<TEntity, TContext>(TContext context)
    : BaseRepositoryWithMapping<TEntity, TContext>(context), INamedEntityRepositoryWithMapping<TEntity>
    where TEntity : class, IEntity, INamedEntity
    where TContext : DbContext
{
    private const string NamedEntityOrdering = "Name, Id";

    public Task<TEntity?> FindByNameAsync(string name, CancellationToken token = default) =>
        FindAsync(entity => string.Equals(entity.Name.ToUpper(), name.ToUpper()), token);

    public Task<TDestination?> FindByNameAsync<TDestination>(string name, IMapper mapper,
        CancellationToken token = default) =>
        FindAsync<TDestination>(entity => string.Equals(entity.Name.ToUpper(), name.ToUpper()), mapper, token);

    public Task<IReadOnlyCollection<TEntity>> GetOrderedListAsync(CancellationToken token = default) =>
        GetListAsync(ordering: NamedEntityOrdering, token);

    public Task<IReadOnlyCollection<TDestination>> GetOrderedListAsync<TDestination>(IMapper mapper,
        CancellationToken token = default) =>
        GetListAsync<TDestination>(ordering: NamedEntityOrdering, mapper, token);

    public Task<IReadOnlyCollection<TEntity>> GetOrderedListAsync(Expression<Func<TEntity, bool>> predicate,
        CancellationToken token = default) =>
        GetListAsync(predicate, ordering: NamedEntityOrdering, token);

    public Task<IReadOnlyCollection<TDestination>> GetOrderedListAsync<TDestination>(
        Expression<Func<TEntity, bool>> predicate, IMapper mapper, CancellationToken token = default) =>
        GetListAsync<TDestination>(predicate, ordering: NamedEntityOrdering, mapper, token);
}
