using AppLibrary.Tests.TestEntities;

namespace AppLibrary.Tests.EfRepositoryTests.NamedEntityRepositoryTests;

public class FindByName : NamedRepositoryTestBase
{
    [Test]
    public async Task FindByName_WhenEntityExists_ReturnsEntity()
    {
        var expected = NamedEntityRepository.Context.Set<TestNamedEntity>().First();

        var result = await NamedEntityRepository.FindByNameAsync(expected.Name);

        result.Should().BeEquivalentTo(expected);
    }

    [Test]
    public async Task FindByName_WhenEntityDoesNotExist_ReturnsNull()
    {
        var result = await NamedEntityRepository.FindByNameAsync("xxx");

        result.Should().BeNull();
    }

    [Test]
    public async Task FindByName_IsNotCaseSensitive()
    {
        var expected = NamedEntityRepository.Context.Set<TestNamedEntity>().First();

        var resultUpper = await NamedEntityRepository.FindByNameAsync(expected.Name.ToUpperInvariant());
        var resultLower = await NamedEntityRepository.FindByNameAsync(expected.Name.ToLowerInvariant());

        using var scope = new AssertionScope();
        resultUpper.Should().BeEquivalentTo(expected);
        resultLower.Should().BeEquivalentTo(expected);
    }
}
