namespace AppLibrary.Tests.LocalRepositoryTests.NamedEntityTests;

public class FindByName : TestsBase
{
    [Test]
    public async Task FindByName_WhenEntityExists_ReturnsEntity()
    {
        var expected = Repository.Items.First();

        var result = await Repository.FindByNameAsync(expected.Name);

        result.Should().BeEquivalentTo(expected);
    }

    [Test]
    public async Task FindByName_WhenEntityDoesNotExist_ReturnsNull()
    {
        var result = await Repository.FindByNameAsync("xxx");

        result.Should().BeNull();
    }

    [Test]
    public async Task FindByName_IsNotCaseSensitive()
    {
        var expected = Repository.Items.First();

        var resultUpper = await Repository.FindByNameAsync(expected.Name.ToUpperInvariant());
        var resultLower = await Repository.FindByNameAsync(expected.Name.ToLowerInvariant());

        using var scope = new AssertionScope();
        resultUpper.Should().BeEquivalentTo(expected);
        resultLower.Should().BeEquivalentTo(expected);
    }
}
