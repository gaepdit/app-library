using AppLibrary.Tests.RepositoryTestHelpers;

namespace AppLibrary.Tests.EfRepositoryTests.NamedEntityTests;

public class GetOrderedList : TestsBase
{
    [Test]
    public async Task GetOrderedList_ReturnsAllEntities()
    {
        var expected = TestData.OrderBy(entity => entity.Name);

        var result = await Repository.GetOrderedListAsync();

        result.Should().BeEquivalentTo(expected);
    }

    [Test]
    public async Task WhenNoItemsExist_ReturnsEmptyList()
    {
        await Helper.ClearTableAsync<TestNamedEntity>();

        var result = await Repository.GetOrderedListAsync();

        result.Should().BeEmpty();
    }

    [Test]
    public async Task GetOrderedList_UsingPredicate_ReturnsCorrectEntities()
    {
        var expected = TestData.Where(entity => entity.Name.Contains(UsefulSuffix)).OrderBy(entity => entity.Name);

        var result = await Repository.GetOrderedListAsync(entity => entity.Name.Contains(UsefulSuffix));

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
