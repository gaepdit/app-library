using AppLibrary.Tests.RepositoryTestHelpers;
using GaEpd.AppLibrary.Domain.Repositories.EFRepository;

namespace AppLibrary.Tests.EfRepositoryTests.NamedEntityTests;

public class NamedEntityRepository(AppDbContext context)
    : NamedEntityRepository<TestNamedEntity, AppDbContext>(context);

public abstract class TestsBase
{
    protected EfRepositoryTestHelper Helper;
    protected NamedEntityRepository Repository;

    protected const string UsefulSuffix = "def";

    protected static readonly List<TestNamedEntity> TestData =
    [
        new(id: Guid.NewGuid(), name: "Abc abc"),
        new(id: Guid.NewGuid(), name: $"Xyx {UsefulSuffix}"),
        new(id: Guid.NewGuid(), name: $"Efg {UsefulSuffix}"),
    ];

    [SetUp]
    public void SetUp()
    {
        Helper = EfRepositoryTestHelper.CreateRepositoryHelper();
        Repository = Helper.GetNamedEntityRepository(TestData);
    }

    [TearDown]
    public async Task TearDown()
    {
        await Repository.DisposeAsync();
        await Helper.DisposeAsync();
    }
}
