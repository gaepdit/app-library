# Changelog

## [7.2.0] - 2026-01-08

- Added support for .NET 10.

## [7.1.0] - 2025-09-29

(All changes since version [7.0.0](#700---2025-08-07))

- Added new overloads to the `FindAsync`, `GetListAsync`, and `GetPagedListAsync` methods in
  `IReadRepositoryWithMapping`. These overloads take two type parameters and allow for projecting to a destination DTO
  from a source entity which is itself a child derived from the base repository entity. I.e.,
  `GetListAsync<TDestination, TSource>(...) where TSource : TEntity;` If your entities use inheritance, this greatly
  simplifies mapping query results.

## [7.1.0-beta.2] - 2025-09-29

- Added new overloads to the `FindAsync` and `GetPagedListAsync` methods in `IReadRepositoryWithMapping` similar to the
  `GetListAsync` methods from the previous beta release.

## [7.1.0-beta.1] - 2025-09-04

- Added new overloads to the `GetListAsync` methods in `IReadRepositoryWithMapping`. These overloads take two type
  parameters and allow for projecting to a destination DTO from a source entity which is itself a child derived from the
  base repository entity. I.e., `GetListAsync<TDestination, TSource>(...) where TSource : TEntity;` If your entities use
  inheritance, this greatly simplifies mapping query results.

## [7.0.0] - 2025-08-07

(All changes since version [6.1.0](#610---2025-06-05))

- Added three new repository interfaces: `IReadRepositoryWithMapping`, `IRepositoryWithMapping`, and
  `INamedEntityRepositoryWithMapping`. These new repositories inherit the existing repositories and add new methods
  enabling [query projection using AutoMapper](https://docs.automapper.io/en/stable/Queryable-Extensions.html). The new
  methods take a destination DTO as a type parameter, enabling Entity Framework to create much more efficient SQL
  queries.
- **Breaking change:** No-tracking queries now use identity resolution if that is the default tracking behavior set for
  the DB context (i.e., `QueryTrackingBehavior.NoTrackingWithIdentityResolution`).
- **Breaking change:** The `PaginatedRequest` class now requires a non-null, non-empty sorting parameter. (Reliable
  pagination requires a defined ordering, and the class now enforces that.)
- **Breaking change:** The `OrderByIf` extension method now returns an `IQueryable` rather than an `IOrderedQueryable`,
  meaning it cannot be chained with the `ThenBy` method. (The `ordering` parameter already accommodates ordering by
  multiple columns, e.g., `source.OrderByIf("Name, Id")`.)

## [6.2.0-beta.2] - 2025-08-01

- **Breaking change:** No-tracking queries now use identity resolution if that is the default tracking behavior set for
  the DB context (i.e., `QueryTrackingBehavior.NoTrackingWithIdentityResolution`).
- **Breaking change from v6.2.0-beta.1:** The overloads introducing DTO query projection have been moved to separate
  repositories, allowing client applications to opt in if desired. The new repositories use the "WithMapping" suffix,
  e.g., `IReadRepositoryWithMapping`.

## [6.2.0-beta.1] - 2025-07-28

- Added overloads to the `Find`, `GetList`, and `GetPagedList` repository methods to enable [query projection using
  AutoMapper](https://docs.automapper.io/en/stable/Queryable-Extensions.html). The new overloads take a destination DTO
  as a type parameter, enabling Entity Framework to create much more efficient SQL queries.
- **Breaking change:** The `PaginatedRequest` class now requires a non-null, non-empty sorting parameter. (Reliable
  pagination requires a defined ordering, and the class now enforces that.)
- **Breaking change:** The `OrderByIf` extension method now returns an `IQueryable` rather than an `IOrderedQueryable`,
  meaning it cannot be chained with the `ThenBy` method. (The `ordering` parameter already accommodates ordering by
  multiple columns.)

## [6.1.0] - 2025-06-05

- **Breaking change:** Increased the number of overloads in the `IReadRepository` interface by using fewer optional
  parameters. (This avoids requiring calls to the read repository methods to be rewritten if updating from a pre-6.0
  version, so for some this is an *unbreaking change*. If you have mocked any `IReadRepository` in your unit tests,
  though, you might have to update which methods are actually being called.)

## [6.0.0] - 2025-03-31

- **Breaking change:** Reduced the number of overloads in the `IReadRepository` interface by using more optional
  parameters. (This may require rewriting calls to the read repository methods.)
- Added optional `includeProperties` parameters to the `GetListAsync()` repository methods.

## [5.6.1] - 2025-03-24

- Added an overload to the `FetchApiDataAsync` method.

## [5.6.0] - 2025-03-24

- Added two utility methods to help with retrieving data from an API:
    - `UriCombine()` combines a base URI and path while correctly handling path separators.
    - `FetchApiDataAsync<T>()` fetches JSON data from an API endpoint and serializes it to a target type.

## [5.5.0] - 2025-01-28

- Added support for .NET 9.

## [5.4.0] - 2025-01-09

- Added `GetListAsync` overloads that allow you to specify the ordering of the returned list.
- Added `GetPagedListAsync` overloads that allow you to specify what navigation properties to include (when using the
  Entity Framework repository).
- The parent type of `EntityNotFoundException` has been changed to `KeyNotFoundException` (instead of `Exception`).

## [5.3.1] - 2024-09-11

- Fixed the Entity Framework repository's `GetAsync` method to enable change tracking for the returned entity.

## [5.3.0] - 2024-09-10

- Added `GetAsync` and `FindAsync` overloads that allow you to specify what navigation properties to include (when using
  the Entity Framework repository).

## [5.2.1] - 2024-04-30

- Added a `GetOrderedListAsync` overload method with predicate matching.

## [5.2.0] - 2024-04-30

- Added a string `Truncate` function.
- Added a `GetOrderedListAsync` method to the Named Entity Repository.

## [5.1.0] - 2024-01-03

- Updated the included GuardClauses library to v2.0.0.

## [5.0.1] - 2024-01-02

- Updated changelog for v5.0.0 release.

## [5.0.0] - 2024-01-02

- Upgraded to .NET 8.0.

### Added

- Added entity and repository interfaces that default to using a GUID primary key and updated the abstract classes to
  use these new interfaces.

### Changed

- **Breaking changes:**
    - Uses of `EntityNotFoundException` will need to be updated to provide the class type. For example,
      `EntityNotFoundException(typeof(MyEntity), id)` should be replaced with `EntityNotFoundException<MyEntity>(id)`.
    - References to `IEntity<Guid>` may need to be replaced with `IEntity`.

## [4.1.0] - 2023-11-09

- Implement IAsyncDisposable in repositories.

## [4.0.0] - 2023-10-25

- Move GuardClauses to a separate NuGet package.

## [3.5.1] - 2023-09-20

- Derived EF repositories can now specify the DbContext type.

## [3.5.0] - 2023-09-19

- Added an abstract StandardNameEntity along with INamedEntity and IActiveEntity interfaces.
- Added INamedEntityRepository and INamedEntityManager interfaces and implementations.

## [3.4.0] - 2023-09-18

- Included abstract implementations of BaseRepository.

## [3.3.0] - 2023-08-11

- Added a "ConcatWithSeparator" string extension.
- Added "PreviousPageNumber" and "NextPageNumber" properties to the IPaginatedResult interface.
- Made some possible performance improvements to the Enum extensions.
  Breaking change: The Enum extensions no longer work with nullable Enum values.

## [3.2.0] - 2023-05-22

- Added a "SetNotDeleted" (undelete) method to the ISoftDelete interface.

## [3.1.0] - 2023-04-25

- Added a "SaveChanges" method to the "write" repository.

## [3.0.0] - 2023-04-25

- Upgraded the library to .NET 7.

## [2.0.0] - 2023-03-07

- Moved the write repository operations to a separate interface.
- Added "Exists" methods to the read repository interface.
- Renamed the user ID properties on auditable entities.

## [1.1.0] - 2023-03-07

- Added predicate builder and common entity filters.
- Added enum extensions.
- Added the System.Linq.Dynamic.Core package.

## [1.0.1] - 2022-10-14

- Added a Readme file to the package.

## [1.0.0] - 2022-10-06

_Initial release._

[1.0.0]: https://github.com/gaepdit/app-library/releases/tag/v1.0.0
[1.0.1]: https://github.com/gaepdit/app-library/releases/tag/v1.0.1
[1.1.0]: https://github.com/gaepdit/app-library/releases/tag/v1.1.0
[2.0.0]: https://github.com/gaepdit/app-library/releases/tag/v2.0.0
[3.0.0]: https://github.com/gaepdit/app-library/releases/tag/v3.0.0
[3.1.0]: https://github.com/gaepdit/app-library/releases/tag/v3.1.0
[3.2.0]: https://github.com/gaepdit/app-library/releases/tag/v3.2.0
[3.3.0]: https://github.com/gaepdit/app-library/releases/tag/v3.3.0
[3.4.0]: https://github.com/gaepdit/app-library/releases/tag/v3.4.0
[3.5.0]: https://github.com/gaepdit/app-library/releases/tag/v3.5.0
[3.5.1]: https://github.com/gaepdit/app-library/releases/tag/v3.5.1
[4.0.0]: https://github.com/gaepdit/app-library/releases/tag/al%2Fv4.0.0
[4.1.0]: https://github.com/gaepdit/app-library/releases/tag/al%2Fv4.1.0
[5.0.0]: https://github.com/gaepdit/app-library/releases/tag/al%2Fv5.0.0
[5.0.1]: https://github.com/gaepdit/app-library/releases/tag/al%2Fv5.0.1
[5.1.0]: https://github.com/gaepdit/app-library/releases/tag/l%2Fv5.1.0
[5.2.0]: https://github.com/gaepdit/app-library/releases/tag/v5.2.0
[5.2.1]: https://github.com/gaepdit/app-library/releases/tag/v5.2.1
[5.3.0]: https://github.com/gaepdit/app-library/releases/tag/v5.3.0
[5.3.1]: https://github.com/gaepdit/app-library/releases/tag/v5.3.1
[5.4.0]: https://github.com/gaepdit/app-library/releases/tag/v5.4.0
[5.5.0]: https://github.com/gaepdit/app-library/releases/tag/v5.5.0
[5.6.0]: https://github.com/gaepdit/app-library/releases/tag/v5.6.0
[5.6.1]: https://github.com/gaepdit/app-library/releases/tag/v5.6.1
[6.0.0]: https://github.com/gaepdit/app-library/releases/tag/v6.0.0
[6.1.0]: https://github.com/gaepdit/app-library/releases/tag/v6.1.0
[6.2.0-beta.1]: https://github.com/gaepdit/app-library/releases/tag/v6.2.0-beta.1
[6.2.0-beta.2]: https://github.com/gaepdit/app-library/releases/tag/v6.2.0-beta.2
[7.0.0]: https://github.com/gaepdit/app-library/releases/tag/v7.0.0
[7.1.0-beta.1]: https://github.com/gaepdit/app-library/releases/tag/v7.1.0-beta.1
[7.1.0-beta.2]: https://github.com/gaepdit/app-library/releases/tag/v7.1.0-beta.2
[7.1.0]: https://github.com/gaepdit/app-library/releases/tag/v7.1.0

[7.2.0]: https://github.com/gaepdit/app-library/releases/tag/v7.2.0
