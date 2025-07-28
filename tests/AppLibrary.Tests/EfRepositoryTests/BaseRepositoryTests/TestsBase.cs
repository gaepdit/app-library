using AppLibrary.Tests.RepositoryTestHelpers;
using GaEpd.AppLibrary.Domain.Repositories.EFRepository;

namespace AppLibrary.Tests.EfRepositoryTests.BaseRepositoryTests;

public class TestRepository(AppDbContext context) : BaseRepository<TestEntity, AppDbContext>(context);

public abstract class TestsBase
{
    protected EfRepositoryTestHelper Helper;
    protected TestRepository Repository;

    protected static readonly List<TestEntity> TestData =
    [
        new() { Id = Guid.NewGuid(), Note = "Abc" },
        new() { Id = Guid.NewGuid(), Note = "Def" },
        new() { Id = Guid.NewGuid(), Note = "Ghi" },
    ];

    [SetUp]
    public void SetUp()
    {
        Helper = EfRepositoryTestHelper.CreateRepositoryHelper();
        Repository = Helper.GetRepository(TestData);
    }

    [TearDown]
    public async Task TearDown()
    {
        await Repository.DisposeAsync();
        await Helper.DisposeAsync();
    }
}
