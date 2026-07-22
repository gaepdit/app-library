# NuGet Package maintenance

Some NuGet packages have been added directly to work around vulnerable dependencies in other packages.

- `System.Security.Cryptography.Xml` 10.0.10 has added to `AppLibrary.Tests` to avoid a vulnerable version in
  `EfCore.TestSupport`.

- `SQLitePCLRaw.lib.e_sqlite3` 2.1.12 has added to `AppLibrary.Tests` to avoid a vulnerable version in
  `EfCore.TestSupport` (via `Microsoft.EntityFrameworkCore.Sqlite`).
