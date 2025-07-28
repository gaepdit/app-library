using AppLibrary.Tests.RepositoryTestHelpers;

namespace AppLibrary.Tests.EfRepositoryTests.ProjectToTests;

public class GetListAndProject : TestsBase
{
    [Test]
    public async Task GetList_ReturnsAllAsDto()
    {
        // Arrange
        var expected = TestData.OrderBy(e => e.Id).ToList();

        // Act
        var result = await Repository.GetListAsync<EntityDto>(Mapper!);

        // Assert
        using var scope = new AssertionScope();
        result.Should().AllBeOfType<EntityDto>();
        var results = result.OrderBy(e => e.Id).ToList();
        for (var i = 0; i < TestData.Count; i++)
        {
            results[i].Id.Should().Be(expected[i].Id);
            results[i].TextRecordText.Should().Be(expected[i].TextRecord.Text);
        }
    }

    [Test]
    public async Task WhenNoItemsExist_ReturnsEmptyList()
    {
        await Helper.ClearTableAsync<EntityWithChildProperty>();

        var result = await Repository.GetListAsync<EntityDto>(Mapper!);

        result.Should().BeEmpty();
    }

    [Test]
    public async Task GetList_UsingPredicate_ReturnsCorrectEntities()
    {
        // Arrange
        var item = TestData[0];

        // Act
        var result = await Repository.GetListAsync<EntityDto>(entity => entity.Id == item.Id, Mapper!);

        // Assert
        using var scope = new AssertionScope();
        result.Should().AllBeOfType<EntityDto>();
        result.First().Id.Should().Be(item.Id);
        result.First().TextRecordText.Should().Be(item.TextRecord.Text);
    }

    [Test]
    public async Task GetList_UsingPredicate_WhenNoItemsMatch_ReturnsEmptyList()
    {
        var id = Guid.NewGuid();

        var result = await Repository.GetListAsync<EntityDto>(entity => entity.Id == id, Mapper!);

        result.Should().BeEmpty();
    }
}
