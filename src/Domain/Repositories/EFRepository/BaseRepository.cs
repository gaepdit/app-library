﻿using GaEpd.AppLibrary.Domain.Entities;
using GaEpd.AppLibrary.Pagination;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace GaEpd.AppLibrary.Domain.Repositories.EFRepository;

public abstract class BaseRepository<TEntity, TKey> : IRepository<TEntity, TKey>
    where TEntity : class, IEntity<TKey>
    where TKey : IEquatable<TKey>
{
    public readonly DbContext Context;

    protected BaseRepository(DbContext context) => Context = context;

    public async Task<TEntity> GetAsync(TKey id, CancellationToken token = default) =>
        await Context.Set<TEntity>().SingleOrDefaultAsync(e => e.Id.Equals(id), token)
        ?? throw new EntityNotFoundException(typeof(TEntity), id);

    public Task<TEntity?> FindAsync(TKey id, CancellationToken token = default) =>
        Context.Set<TEntity>().AsNoTracking().SingleOrDefaultAsync(e => e.Id.Equals(id), token);

    public Task<TEntity?> FindAsync(
        Expression<Func<TEntity, bool>> predicate, CancellationToken token = default) =>
        Context.Set<TEntity>().AsNoTracking().SingleOrDefaultAsync(predicate, token);

    public async Task<IReadOnlyCollection<TEntity>> GetListAsync(CancellationToken token = default) =>
        await Context.Set<TEntity>().AsNoTracking().ToListAsync(token);

    public async Task<IReadOnlyCollection<TEntity>> GetListAsync(
        Expression<Func<TEntity, bool>> predicate, CancellationToken token = default) =>
        await Context.Set<TEntity>().AsNoTracking().Where(predicate).ToListAsync(token);

    public async Task<IReadOnlyCollection<TEntity>> GetPagedListAsync(
        Expression<Func<TEntity, bool>> predicate, PaginatedRequest paging, CancellationToken token = default) =>
        await Context.Set<TEntity>().AsNoTracking().Where(predicate)
            .OrderByIf(paging.Sorting).Skip(paging.Skip).Take(paging.Take).ToListAsync(token);

    public async Task<IReadOnlyCollection<TEntity>> GetPagedListAsync(
        PaginatedRequest paging, CancellationToken token = default) =>
        await Context.Set<TEntity>().AsNoTracking()
            .OrderByIf(paging.Sorting).Skip(paging.Skip).Take(paging.Take).ToListAsync(token);

    public Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken token = default) =>
        Context.Set<TEntity>().AsNoTracking().CountAsync(predicate, token);

    public Task<bool> ExistsAsync(TKey id, CancellationToken token = default) =>
        Context.Set<TEntity>().AsNoTracking().AnyAsync(e => e.Id.Equals(id), token);

    public Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken token = default) =>
        Context.Set<TEntity>().AsNoTracking().AnyAsync(predicate, token);

    public async Task InsertAsync(TEntity entity, bool autoSave = true, CancellationToken token = default)
    {
        await Context.Set<TEntity>().AddAsync(entity, token);
        if (autoSave) await SaveChangesAsync(token);
    }

    public async Task UpdateAsync(TEntity entity, bool autoSave = true, CancellationToken token = default)
    {
        Context.Attach(entity);
        Context.Update(entity);

        if (!autoSave) return;

        try
        {
            await SaveChangesAsync(token);
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await Context.Set<TEntity>().AsNoTracking().AnyAsync(e => e.Id.Equals(entity.Id), token))
                throw new EntityNotFoundException(typeof(TEntity), entity.Id);
            throw;
        }
    }

    public async Task DeleteAsync(TEntity entity, bool autoSave = true, CancellationToken token = default)
    {
        Context.Set<TEntity>().Remove(entity);

        try
        {
            if (autoSave) await SaveChangesAsync(token);
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await Context.Set<TEntity>().AsNoTracking().AnyAsync(e => e.Id.Equals(entity.Id), token))
            {
                throw new EntityNotFoundException(typeof(TEntity), entity.Id);
            }

            throw;
        }
    }

    public async Task SaveChangesAsync(CancellationToken token = default) => await Context.SaveChangesAsync(token);

    // ReSharper disable once VirtualMemberNeverOverridden.Global
    protected virtual void Dispose(bool disposing)
    {
        if (disposing) Context.Dispose();
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}
