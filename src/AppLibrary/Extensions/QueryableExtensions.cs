using GaEpd.AppLibrary.Pagination;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;

namespace GaEpd.AppLibrary.Extensions;

public static class QueryableExtensions
{
    public static IQueryable<TSource> WhereIf<TSource>(this IQueryable<TSource> source,
        Expression<Func<TSource, bool>>? predicate) =>
        predicate is null ? source : source.Where(predicate);

    public static IQueryable<TSource> WhereIf<TSource>(this IEnumerable<TSource> source,
        Expression<Func<TSource, bool>>? predicate) =>
        (predicate is null ? source : source.Where(predicate.Compile())).AsQueryable();

    public static IQueryable<TSource> OrderByIf<TSource>(this IQueryable<TSource> source, string? ordering) =>
        string.IsNullOrWhiteSpace(ordering) ? source : source.OrderBy(ordering);

    public static IQueryable<TSource> ApplyPaging<TSource>(this IQueryable<TSource> queryable,
        PaginatedRequest paging) =>
        queryable.OrderBy(paging.Sorting).Skip(paging.Skip).Take(paging.Take);
}
