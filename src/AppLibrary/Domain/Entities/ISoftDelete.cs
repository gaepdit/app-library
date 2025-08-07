﻿namespace GaEpd.AppLibrary.Domain.Entities;

/// <summary>
/// Marks an entity for enabling soft deletion in the database.
/// </summary>
public interface ISoftDelete
{
    /// <summary>
    /// A flag indicating whether the entity should be considered as having been deleted.
    /// </summary>
    bool IsDeleted { get; }
    /// <summary>
    /// The date and time the entity was deleted.
    /// </summary>
    DateTimeOffset? DeletedAt { get; }
}

/// <summary>
/// Marks an entity for enabling soft deletion in the database and records the ID of the user who deleted the entity.
/// </summary>
public interface ISoftDelete<TUserKey> : ISoftDelete
    where TUserKey : IEquatable<TUserKey>
{
    /// <summary>
    /// The ID of the user who deleted the entity.
    /// </summary>
    TUserKey? DeletedById { get; }

    /// <summary>
    /// Sets the <see cref="ISoftDelete.IsDeleted"/> property to "true", indicating that the element should be
    /// considered to have been deleted. Also sets the <see cref="DeletedById"/> property to the
    /// <paramref cref="userId"/> parameter value and the <see cref="ISoftDelete.DeletedAt"/> property to the current
    /// <see cref="DateTimeOffset"/> value.
    /// </summary>
    /// <param name="userId">The ID of the user deleting the entity.</param>
    void SetDeleted(TUserKey? userId);

    /// <summary>
    /// Sets the <see cref="ISoftDelete.IsDeleted"/> property to "false", indicating that the element should NOT be
    /// considered to have been deleted. Also clears the <see cref="DeletedById"/> and
    /// <see cref="ISoftDelete.DeletedAt"/> properties.
    /// </summary>
    void SetNotDeleted();
}
