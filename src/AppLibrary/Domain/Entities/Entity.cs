namespace GaEpd.AppLibrary.Domain.Entities;

/// <summary>
/// An implementation of <see cref="IEntity{TKey}"/>.
/// </summary>
/// <typeparam name="TKey">The type of the primary key.</typeparam>
public abstract class Entity<TKey> : IEntity<TKey>
    where TKey : IEquatable<TKey>
{
    private TKey? _id;

    public TKey Id
    {
        protected set => _id = Guard.NotNull(value);
        get => _id ?? throw new InvalidOperationException($"Uninitialized property: {nameof(Id)}");
    }

    protected Entity() { }
    protected Entity(TKey id) => Id = id;
}

/// <summary>
/// The default implementation of <see cref="Entity{TKey}"/> using <see cref="Guid"/> for the Entity
/// primary key.
/// </summary>
public abstract class Entity : Entity<Guid>, IEntity;
