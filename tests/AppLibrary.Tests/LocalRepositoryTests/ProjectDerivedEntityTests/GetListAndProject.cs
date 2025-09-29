using AppLibrary.Tests.RepositoryTestHelpers;

namespace AppLibrary.Tests.LocalRepositoryTests.ProjectDerivedEntityTests;

public class GetListAndProject : TestsBase
{
    [Test]
    public async Task GetList_ReturnsAllAsDto()
    {
        // Arrange
        var expected = TestData.Where(e => e is EntityWithChildProperty);

        // Act
        var result = await Repository.GetListAsync<EntityDto, EntityWithChildProperty>(Mapper!);

        // Assert
        using var scope = new AssertionScope();
        result.Should().AllBeOfType<EntityDto>();
        result.Should().BeEquivalentTo(expected);
    }

    [Test]
    public async Task WhenNoItemsExist_ReturnsEmptyList()
    {
        // Arrange
        Repository.Items.Clear();

        // Act
        var result = await Repository.GetListAsync<EntityDto, EntityWithChildProperty>(Mapper!);

        // Assert
        result.Should().BeEmpty();
    }

    [Test]
    public async Task GetList_UsingPredicate_ReturnsCorrectEntitiesAsDto()
    {
        // Arrange
        var expected = TestData.First(e => e is EntityWithChildProperty);

        // Act
        var result =
            await Repository.GetListAsync<EntityDto, EntityWithChildProperty>(entity => entity.Id == expected.Id,
                Mapper!);

        // Assert
        result.Single().Should().BeEquivalentTo(expected);
    }

    [Test]
    public async Task GetList_UsingPredicate_WhenNoItemsMatch_ReturnsEmptyList()
    {
        // Arrange
        var id = Guid.NewGuid();

        // Act
        var result =
            await Repository.GetListAsync<EntityDto, EntityWithChildProperty>(entity => entity.Id == id, Mapper!);

        // Assert
        result.Should().BeEmpty();
    }
}
