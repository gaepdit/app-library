using AppLibrary.Tests.RepositoryTestHelpers;
using AutoMapper;
using GaEpd.AppLibrary.Domain.Repositories.EFRepository;

namespace AppLibrary.Tests.EfRepositoryTests.ProjectToTests;

public class ProjectToRepository(AppDbContext context)
    : BaseRepositoryWithMapping<EntityWithChildProperty, AppDbContext>(context);

public abstract class TestsBase
{
    protected EfRepositoryTestHelper Helper;
    protected ProjectToRepository Repository;
    protected static IMapper? Mapper { get; private set; }

    protected static readonly List<EntityWithChildProperty> TestData =
    [
        new()
        {
            Id = Guid.NewGuid(),
            Note = "A",
            TextRecord = new TextRecord { Id = Guid.NewGuid(), Text = "Abc" },
        },

        new()
        {
            Id = Guid.NewGuid(),
            Note = "B",
            TextRecord = new TextRecord { Id = Guid.NewGuid(), Text = "Def" },
        },
    ];

    [SetUp]
    public void SetUp()
    {
        Helper = EfRepositoryTestHelper.CreateRepositoryHelper();
        Repository = Helper.GetProjectToRepository(TestData);
    }

    [TearDown]
    public async Task TearDown()
    {
        await Repository.DisposeAsync();
        await Helper.DisposeAsync();
    }

    [OneTimeSetUp]
    public void OneTimeSetUp() => Mapper = new MapperConfiguration(configuration =>
        configuration.AddProfile(new TestAutoMapperProfile())).CreateMapper();
}
