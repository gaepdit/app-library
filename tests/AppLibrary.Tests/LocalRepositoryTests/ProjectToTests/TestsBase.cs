using AppLibrary.Tests.RepositoryTestHelpers;
using AutoMapper;
using GaEpd.AppLibrary.Domain.Repositories.LocalRepository;

namespace AppLibrary.Tests.LocalRepositoryTests.ProjectToTests;

public class TestRepository(IEnumerable<EntityWithChildProperty> items)
    : BaseRepositoryWithMapping<EntityWithChildProperty, Guid>(items)
{
    public static TestRepository GetRepository(IEnumerable<EntityWithChildProperty> testData) => new(testData);
}

[TestFixture]
public abstract class TestsBase
{
    protected TestRepository Repository { get; private set; }
    protected static IMapper? Mapper { get; private set; }

    protected static readonly List<EntityWithChildProperty> TestData =
    [
        new()
        {
            Id = Guid.NewGuid(),
            Note = "Llama",
            TextRecord = new TextRecord { Id = Guid.NewGuid(), Text = "Abc" },
        },
        new()
        {
            Id = Guid.NewGuid(),
            Note = "Moose",
            TextRecord = new TextRecord { Id = Guid.NewGuid(), Text = "Def" },
        },
        new()
        {
            Id = Guid.NewGuid(),
            Note = "Narwhal",
            TextRecord = new TextRecord { Id = Guid.NewGuid(), Text = "Ghi" },
        },
    ];

    [SetUp]
    public void SetUp() => Repository = TestRepository.GetRepository(TestData);

    [TearDown]
    public async Task TearDown() => await Repository.DisposeAsync();

    [OneTimeSetUp]
    public void OneTimeSetUp() => Mapper = new MapperConfiguration(configuration =>
        configuration.AddProfile(new TestAutoMapperProfile())).CreateMapper();
}
