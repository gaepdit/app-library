using GaEpd.AppLibrary.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace GaEpd.AppLibrary.Domain.Repositories.EFRepository;

/// <summary>
/// An implementation of <see cref="IRepository{TEntity,TKey}"/> using Entity Framework where TKey is
/// a <see cref="Guid"/> primary key.
/// </summary>
/// <typeparam name="TEntity">The entity type.</typeparam>
/// <typeparam name="TContext">The type of the <see cref="DbContext"/>.</typeparam>
public abstract class BaseRepositoryWithMapping<TEntity, TContext>(TContext context)
    : BaseRepositoryWithMapping<TEntity, Guid, DbContext>(context), IRepositoryWithMapping<TEntity>
    where TEntity : class, IEntity
    where TContext : DbContext;

/// <summary>
/// An implementation of <see cref="IRepository{TEntity,TKey}"/> using Entity Framework.
/// </summary>
/// <typeparam name="TEntity">The entity type.</typeparam>
/// <typeparam name="TKey">The primary key type for the entity.</typeparam>
/// <typeparam name="TContext">The type of the <see cref="DbContext"/>.</typeparam>
[SuppressMessage("", "S2436")]
public abstract partial class BaseRepositoryWithMapping<TEntity, TKey, TContext>(TContext context)
    : BaseRepository<TEntity, TKey, TContext>(context), IRepositoryWithMapping<TEntity, TKey>
    where TEntity : class, IEntity<TKey>
    where TKey : IEquatable<TKey>
    where TContext : DbContext { }
