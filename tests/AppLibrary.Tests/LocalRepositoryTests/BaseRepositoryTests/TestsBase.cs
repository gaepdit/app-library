using AppLibrary.Tests.RepositoryTestHelpers;
using GaEpd.AppLibrary.Domain.Repositories.LocalRepository;

namespace AppLibrary.Tests.LocalRepositoryTests.BaseRepositoryTests;

public class TestRepository(IEnumerable<TestEntity> items) : BaseRepository<TestEntity, Guid>(items)
{
    public static TestRepository GetRepository(List<TestEntity> testData) => new(testData);
}

public abstract class TestsBase
{
    protected TestRepository Repository;

    private static readonly List<TestEntity> TestData =
    [
        new() { Id = Guid.NewGuid(), Note = "Abc" },
        new() { Id = Guid.NewGuid(), Note = "Def" },
        new() { Id = Guid.NewGuid(), Note = "Ghi" },
    ];

    [SetUp]
    public void SetUp() => Repository = TestRepository.GetRepository(TestData);

    [TearDown]
    public async Task TearDown() => await Repository.DisposeAsync();
}
