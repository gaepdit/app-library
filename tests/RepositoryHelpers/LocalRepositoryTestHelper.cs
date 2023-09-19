﻿using GaEpd.AppLibrary.Domain.Repositories.LocalRepository;
using GaEpd.AppLibrary.Tests.EntityHelpers;

namespace GaEpd.AppLibrary.Tests.RepositoryHelpers;

public class LocalRepository : BaseRepository<TestEntity, Guid>
{
    public LocalRepository(IEnumerable<TestEntity> items) : base(items) { }
}

public class LocalNamedEntityRepository : NamedEntityRepository<TestNamedEntity>
{
    public LocalNamedEntityRepository(IEnumerable<TestNamedEntity> items) : base(items) { }
}

public static class LocalRepositoryTestHelper
{
    public static LocalRepository GetTestRepository() =>
        new(new List<TestEntity>
        {
            new() { Id = Guid.NewGuid(), Name = "Abc" },
            new() { Id = Guid.NewGuid(), Name = "Def" },
        });

    public static LocalNamedEntityRepository GetNamedEntityRepository() =>
        new(new List<TestNamedEntity>
        {
            new(Guid.NewGuid(), "Abcd"),
            new(Guid.NewGuid(), "Efgh"),
        });
}
