using AppLibrary.Tests.RepositoryTestHelpers;
using GaEpd.AppLibrary.Domain.Repositories.EFRepository;

namespace AppLibrary.Tests.EfRepositoryTests.IncludePropertiesTests;

public class NavigationPropertiesRepository(AppDbContext context)
    : BaseRepository<EntityWithNavigationProperty, AppDbContext>(context);

public abstract class TestsBase
{
    protected const string TextRecordsName = nameof(EntityWithNavigationProperty.TextRecords);
    protected EfRepositoryTestHelper Helper;
    protected NavigationPropertiesRepository Repository;

    protected static readonly List<EntityWithNavigationProperty> TestData =
    [
        new()
        {
            Id = Guid.NewGuid(),
            Note = "Moose",
            TextRecords =
            {
                new TextRecord { Id = Guid.NewGuid(), Text = "A" },
                new TextRecord { Id = Guid.NewGuid(), Text = "B" },
            },
        },
        new()
        {
            Id = Guid.NewGuid(),
            Note = "Narwhal",
            TextRecords =
            {
                new TextRecord { Id = Guid.NewGuid(), Text = "A" },
                new TextRecord { Id = Guid.NewGuid(), Text = "B" },
            },
        },
    ];

    [SetUp]
    public void SetUp()
    {
        Helper = EfRepositoryTestHelper.CreateRepositoryHelper();
        Repository = Helper.GetNavigationPropertiesRepository(TestData);
    }

    [TearDown]
    public async Task TearDown()
    {
        await Repository.DisposeAsync();
        await Helper.DisposeAsync();
    }
}
