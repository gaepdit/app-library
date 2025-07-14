using AppLibrary.Tests.RepositoryTestHelpers;
using AutoMapper;
using GaEpd.AppLibrary.Domain.Repositories.LocalRepository;

namespace AppLibrary.Tests.LocalRepositoryTests.NamedEntityProjectToTests;

public class NamedEntityWithChildPropertyRepository(IEnumerable<NamedEntityWithChildProperty> items)
    : NamedEntityRepository<NamedEntityWithChildProperty>(items)
{
    public static NamedEntityWithChildPropertyRepository GetRepository(
        IEnumerable<NamedEntityWithChildProperty> testData) => new(testData);
}

public abstract class TestsBase
{
    protected NamedEntityWithChildPropertyRepository Repository;
    protected static IMapper? Mapper { get; private set; }

    protected const string UsefulSuffix = "def";

    public static readonly List<NamedEntityWithChildProperty> TestData =
    [
        new(id: Guid.NewGuid(), name: "Abc abc")
            { TextRecord = new TextRecord { Id = Guid.NewGuid(), Text = "Apple" } },
        new(id: Guid.NewGuid(), name: $"Xyx {UsefulSuffix}")
            { TextRecord = new TextRecord { Id = Guid.NewGuid(), Text = "Banana" } },
        new(id: Guid.NewGuid(), name: $"Efg {UsefulSuffix}")
            { TextRecord = new TextRecord { Id = Guid.NewGuid(), Text = "Cookie" } },
    ];

    [SetUp]
    public void SetUp() => Repository = NamedEntityWithChildPropertyRepository.GetRepository(TestData);

    [TearDown]
    public async Task TearDown() => await Repository.DisposeAsync();

    [OneTimeSetUp]
    public void OneTimeSetUp() =>
        Mapper = new MapperConfiguration(configuration => configuration.AddProfile(new TestAutoMapperProfile()))
            .CreateMapper();
}
