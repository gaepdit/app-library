using AppLibrary.Tests.RepositoryTestHelpers;
using GaEpd.AppLibrary.Domain.Repositories.LocalRepository;

namespace AppLibrary.Tests.LocalRepositoryTests.NamedEntityTests;

public class NamedEntityRepository(IEnumerable<TestNamedEntity> items) : NamedEntityRepository<TestNamedEntity>(items)
{
    public static NamedEntityRepository GetRepository(IEnumerable<TestNamedEntity> testData) => new(testData);
}

public abstract class TestsBase
{
    protected NamedEntityRepository Repository;

    protected const string UsefulSuffix = "def";

    public static readonly List<TestNamedEntity> TestData =
    [
        new(id: Guid.NewGuid(), name: "Abc abc"),
        new(id: Guid.NewGuid(), name: $"Xyx {UsefulSuffix}"),
        new(id: Guid.NewGuid(), name: $"Efg {UsefulSuffix}"),
    ];

    [SetUp]
    public void SetUp() => Repository = NamedEntityRepository.GetRepository(TestData);

    [TearDown]
    public async Task TearDown() => await Repository.DisposeAsync();
}
