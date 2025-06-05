namespace AppLibrary.Tests.EfRepositoryTests.NamedEntityRepositoryTests;

public abstract class NamedRepositoryTestBase
{
    protected EfRepositoryTestHelper Helper;

    protected TestNamedEntityRepository NamedEntityRepository;

    [SetUp]
    public void SetUp()
    {
        Helper = EfRepositoryTestHelper.CreateRepositoryHelper();
        NamedEntityRepository = Helper.GetNamedEntityRepository();
    }

    [TearDown]
    public async Task TearDown()
    {
        await NamedEntityRepository.DisposeAsync();
        await Helper.DisposeAsync();
    }
}
