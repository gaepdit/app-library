namespace AppLibrary.Tests.LocalRepositoryTests.NamedEntityTests;

public class GetOrderedList : TestsBase
{
    [Test]
    public async Task GetOrderedList_ReturnsAllEntities()
    {
        var result = await Repository.GetOrderedListAsync();

        result.Should().BeEquivalentTo(Repository.Items.OrderBy(entity => entity.Name));
    }

    [Test]
    public async Task WhenNoItemsExist_ReturnsEmptyList()
    {
        Repository.Items.Clear();

        var result = await Repository.GetOrderedListAsync();

        result.Should().BeEmpty();
    }

    [Test]
    public async Task GetOrderedList_UsingPredicate_ReturnsCorrectEntities()
    {
        var expected = Repository.Items
            .Where(entity => entity.Name.Contains(UsefulSuffix))
            .OrderBy(entity => entity.Name);

        var result = await Repository.GetOrderedListAsync(entity => entity.Name.Contains("def"));

        result.Should().BeEquivalentTo(expected);
    }

    [Test]
    public async Task GetOrderedList_UsingPredicate_WhenNoItemsMatch_ReturnsEmptyList()
    {
        var id = Guid.NewGuid();

        var result = await Repository.GetOrderedListAsync(entity => entity.Id == id);

        result.Should().BeEmpty();
    }
}
