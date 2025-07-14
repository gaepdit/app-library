using AppLibrary.Tests.RepositoryTestHelpers;

namespace AppLibrary.Tests.LocalRepositoryTests.ProjectToTests;

public class GetListAndProject : TestsBase
{
    [Test]
    public async Task GetList_ReturnsAllAsDto()
    {
        // Act
        var result = await Repository.GetListAsync<EntityDto>(Mapper!);

        // Assert
        using var scope = new AssertionScope();
        result.Should().AllBeOfType<EntityDto>();
        var results = result.ToList();
        for (var i = 0; i < TestData.Count; i++)
        {
            results[i].Id.Should().Be(TestData[i].Id);
            results[i].TextRecordText.Should().Be(TestData[i].TextRecord.Text);
        }
    }

    [Test]
    public async Task WhenNoItemsExist_ReturnsEmptyList()
    {
        Repository.Items.Clear();

        var result = await Repository.GetListAsync<EntityDto>(Mapper!);

        result.Should().BeEmpty();
    }

    [Test]
    public async Task GetList_UsingPredicate_ReturnsCorrectEntitiesAsDto()
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
