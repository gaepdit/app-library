namespace AppLibrary.Tests.LocalRepositoryTests;

public abstract class RepositoryTestBase
{
    protected TestRepository Repository;

    [SetUp]
    public void SetUp() => Repository = TestRepository.GetRepository();

    [TearDown]
    public async Task TearDown() => await Repository.DisposeAsync();
}
