using AppLibrary.Tests.RepositoryTestHelpers;
using AutoMapper;
using GaEpd.AppLibrary.Domain.Repositories.EFRepository;

namespace AppLibrary.Tests.EfRepositoryTests.NamedEntityProjectToTests;

public class NamedEntityWithChildPropertyRepository(AppDbContext context)
    : NamedEntityRepository<NamedEntityWithChildProperty, AppDbContext>(context);

public abstract class TestsBase
{
    protected EfRepositoryTestHelper Helper;
    protected NamedEntityWithChildPropertyRepository Repository;
    protected static IMapper? Mapper { get; private set; }

    protected const string UsefulSuffix = "def";

    protected static readonly List<NamedEntityWithChildProperty> TestDataWithChildProperties =
    [
        new(id: Guid.NewGuid(), name: "Abc abc")
            { TextRecord = new TextRecord { Id = Guid.NewGuid(), Text = "Apple" } },
        new(id: Guid.NewGuid(), name: $"Xyx {UsefulSuffix}")
            { TextRecord = new TextRecord { Id = Guid.NewGuid(), Text = "Banana" } },
        new(id: Guid.NewGuid(), name: $"Efg {UsefulSuffix}")
            { TextRecord = new TextRecord { Id = Guid.NewGuid(), Text = "Cookie" } },
    ];

    [SetUp]
    public void SetUp()
    {
        Helper = EfRepositoryTestHelper.CreateRepositoryHelper();
        Repository = Helper.GetNamedEntityWithChildPropertyRepository(TestDataWithChildProperties);
    }

    [TearDown]
    public async Task TearDown()
    {
        await Repository.DisposeAsync();
        await Helper.DisposeAsync();
    }

    [OneTimeSetUp]
    public void OneTimeSetUp() =>
        Mapper = new MapperConfiguration(configuration => configuration.AddProfile(new TestAutoMapperProfile()))
            .CreateMapper();
}
