using AppLibrary.Tests.RepositoryTestHelpers;
using AutoMapper;
using GaEpd.AppLibrary.Domain.Repositories.EFRepository;

namespace AppLibrary.Tests.EfRepositoryTests.ProjectDerivedEntityTests;

public class ProjectDerivedEntityRepository(AppDbContext context)
    : BaseRepositoryWithMapping<EntityWithChildProperty, AppDbContext>(context);

public abstract class TestsBase
{
    protected EfRepositoryTestHelper Helper;
    protected ProjectDerivedEntityRepository Repository;
    protected static IMapper? Mapper { get; private set; }

    protected static readonly List<TestEntity> TestData =
    [
        new()
        {
            Id = Guid.NewGuid(),
            Note = "Apple",
        },
        new EntityWithChildProperty
        {
            Id = Guid.NewGuid(),
            Note = "Llama",
            TextRecord = new TextRecord { Id = Guid.NewGuid(), Text = "Abc" },
        },
        new()
        {
            Id = Guid.NewGuid(),
            Note = "Banana",
        },
        new EntityWithChildProperty
        {
            Id = Guid.NewGuid(),
            Note = "Moose",
            TextRecord = new TextRecord { Id = Guid.NewGuid(), Text = "Def" },
        },
        new()
        {
            Id = Guid.NewGuid(),
            Note = "Crabapple",
        },
        new EntityWithChildProperty
        {
            Id = Guid.NewGuid(),
            Note = "Narwhal",
            TextRecord = new TextRecord { Id = Guid.NewGuid(), Text = "Ghi" },
        },
    ];

    [SetUp]
    public void SetUp()
    {
        Helper = EfRepositoryTestHelper.CreateRepositoryHelper();
        Repository = Helper.GetProjectDerivedEntityRepository(TestData);
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
