using AppLibrary.Tests.RepositoryTestHelpers;

namespace AppLibrary.Tests.LocalRepositoryTests.NamedEntityProjectToTests;

public class GetOrderedListAndProject : TestsBase
{
    [Test]
    public async Task GetOrderedList_ReturnsAllEntitiesAsDto()
    {
        // Arrange
        var expected = TestData.OrderBy(e => e.Name).ToList();

        // Act
        var result = await Repository.GetOrderedListAsync<NamedEntityWithChildPropertyDto>(Mapper!);

        // Assert
        using var scope = new AssertionScope();
        result.Should().AllBeOfType<NamedEntityWithChildPropertyDto>();
        var results = result.ToList();
        for (var i = 0; i < expected.Count; i++)
        {
            results[i].Id.Should().Be(expected[i].Id);
            results[i].TextRecordText.Should().Be(expected[i].TextRecord!.Text);
        }
    }

    [Test]
    public async Task WhenNoItemsExist_ReturnsEmptyList()
    {
        Repository.Items.Clear();

        var result = await Repository.GetOrderedListAsync<NamedEntityWithChildPropertyDto>(Mapper!);

        result.Should().BeEmpty();
    }

    [Test]
    public async Task GetOrderedList_UsingPredicate_ReturnsCorrectEntities()
    {
        // Arrange
        var expected = TestData
            .Where(entity => entity.Name.Contains(UsefulSuffix))
            .OrderBy(entity => entity.Name).ToList();

        // Act
        var result =
            await Repository.GetOrderedListAsync<NamedEntityWithChildPropertyDto>(
                entity => entity.Name.Contains(UsefulSuffix), Mapper!);

        // Assert
        using var scope = new AssertionScope();
        result.Should().AllBeOfType<NamedEntityWithChildPropertyDto>();
        var results = result.ToList();
        for (var i = 0; i < expected.Count; i++)
        {
            results[i].Id.Should().Be(expected[i].Id);
            results[i].TextRecordText.Should().Be(expected[i].TextRecord!.Text);
        }
    }

    [Test]
    public async Task GetOrderedList_UsingPredicate_WhenNoItemsMatch_ReturnsEmptyList()
    {
        var id = Guid.NewGuid();

        var result =
            await Repository.GetOrderedListAsync<NamedEntityWithChildPropertyDto>(entity => entity.Id == id, Mapper!);

        result.Should().BeEmpty();
    }
}
