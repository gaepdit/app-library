using AppLibrary.Tests.RepositoryTestHelpers;

namespace AppLibrary.Tests.EfRepositoryTests.ProjectDerivedEntityTests;

public class GetListAndProject : TestsBase
{
    [Test]
    public async Task GetList_ReturnsAllAsDto()
    {
        // Act
        var result = await Repository.GetListAsync<EntityDto, EntityWithChildProperty>(Mapper!);

        // Assert
        using var scope = new AssertionScope();
        result.Should().AllBeOfType<EntityDto>();
        result.Should().BeEquivalentTo(TestData.OfType<EntityWithChildProperty>(),
            options => options.Excluding(e => e.TextRecord));
        result.Select(e => e.TextRecordText).Should()
            .BeEquivalentTo(TestData.OfType<EntityWithChildProperty>().Select(e => e.TextRecord.Text));
    }

    [Test]
    public async Task WhenNoItemsExist_ReturnsEmptyList()
    {
        // Arrange
        await Helper.ClearTableAsync<EntityWithChildProperty>();

        // Act
        var result = await Repository.GetListAsync<EntityDto, EntityWithChildProperty>(Mapper!);

        // Assert
        result.Should().BeEmpty();
    }

    [Test]
    public async Task GetList_UsingPredicate_ReturnsCorrectEntities()
    {
        // Arrange
        var item = TestData.OfType<EntityWithChildProperty>().First();

        // Act
        var result =
            await Repository.GetListAsync<EntityDto, EntityWithChildProperty>(
                entity => entity.Id == item.Id, Mapper!);

        // Assert
        using var scope = new AssertionScope();
        result.Single().Should().BeEquivalentTo(item, options => options.Excluding(e => e.TextRecord));
        result.Single().TextRecordText.Should().Be(item.TextRecord.Text);
    }

    [Test]
    public async Task GetList_UsingPredicate_WhenNoItemsMatch_ReturnsEmptyList()
    {
        // Arrange
        var id = Guid.NewGuid();

        // Act
        var result =
            await Repository.GetListAsync<EntityDto, EntityWithChildProperty>(
                entity => entity.Id == id, Mapper!);

        // Assert
        result.Should().BeEmpty();
    }
}
