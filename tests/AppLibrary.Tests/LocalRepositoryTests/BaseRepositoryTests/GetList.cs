namespace AppLibrary.Tests.LocalRepositoryTests.BaseRepositoryTests;

public class GetList : TestsBase
{
    [Test]
    public async Task GetList_ReturnsAllEntities()
    {
        var result = await Repository.GetListAsync();

        result.Should().BeEquivalentTo(Repository.Items);
    }

    [Test]
    public async Task WhenNoItemsExist_ReturnsEmptyList()
    {
        Repository.Items.Clear();

        var result = await Repository.GetListAsync();

        result.Should().BeEmpty();
    }

    [Test]
    public async Task GetList_UsingPredicate_ReturnsCorrectEntities()
    {
        var skipId = Repository.Items.First().Id;
        var expected = Repository.Items.Where(entity => entity.Id != skipId);

        var result = await Repository.GetListAsync(entity => entity.Id != skipId);

        result.Should().BeEquivalentTo(expected);
    }

    [Test]
    public async Task GetList_UsingPredicate_WhenNoItemsMatch_ReturnsEmptyList()
    {
        var id = Guid.NewGuid();

        var result = await Repository.GetListAsync(entity => entity.Id == id);

        result.Should().BeEmpty();
    }
}
