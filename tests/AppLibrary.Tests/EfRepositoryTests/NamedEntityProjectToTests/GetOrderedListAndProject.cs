using AppLibrary.Tests.RepositoryTestHelpers;

namespace AppLibrary.Tests.EfRepositoryTests.NamedEntityProjectToTests;

public class GetOrderedListAndProject : TestsBase
{
    [Test]
    public async Task GetOrderedList_ReturnsAllEntitiesAsDto()
    {
        // Arrange
        var expected = TestDataWithChildProperties.OrderBy(entity => entity.Name).ToList();

        // Act
        var result = await Repository
            .GetOrderedListAsync<NamedEntityWithChildPropertyDto>(Mapper!);

        // Assert
        using var scope = new AssertionScope();
        result.Should().AllBeOfType<NamedEntityWithChildPropertyDto>();
        result.Should().BeEquivalentTo(Mapper!.Map<List<NamedEntityWithChildPropertyDto>>(expected));
    }

    [Test]
    public async Task WhenNoItemsExist_ReturnsEmptyList()
    {
        await Helper.ClearTableAsync<NamedEntityWithChildProperty>();

        var result = await Repository
            .GetOrderedListAsync<NamedEntityWithChildPropertyDto>(Mapper!);

        result.Should().BeEmpty();
    }

    [Test]
    public async Task GetOrderedList_UsingPredicate_ReturnsCorrectEntities()
    {
        // Arrange
        var expected = TestDataWithChildProperties
            .Where(entity => entity.Name.Contains(UsefulSuffix))
            .OrderBy(entity => entity.Name).ToList();

        // Act
        var result = await Repository
            .GetOrderedListAsync<NamedEntityWithChildPropertyDto>(
                entity => entity.Name.Contains(UsefulSuffix), Mapper!);

        // Assert
        using var scope = new AssertionScope();
        result.Should().AllBeOfType<NamedEntityWithChildPropertyDto>();
        result.Should().BeEquivalentTo(Mapper!.Map<List<NamedEntityWithChildPropertyDto>>(expected));
    }

    [Test]
    public async Task GetOrderedList_UsingPredicate_WhenNoItemsMatch_ReturnsEmptyList()
    {
        var id = Guid.NewGuid();

        var result = await Repository.GetOrderedListAsync(entity => entity.Id == id);

        result.Should().BeEmpty();
    }
}
