using AppLibrary.Tests.RepositoryTestHelpers;
using GaEpd.AppLibrary.Pagination;

namespace AppLibrary.Tests.LocalRepositoryTests.ProjectToTests;

public class GetPagedListAndProject : TestsBase
{
    [Test]
    public async Task GetPagedList_ReturnsCorrectPagedResults()
    {
        // Arrange
        var paging = new PaginatedRequest(2, 1, "Id");
        var expected = TestData.OrderBy(entity => entity.Id).Skip(paging.Skip).Take(paging.Take).ToList();

        // Act
        var result = await Repository.GetPagedListAsync<EntityDto>(paging, Mapper!);

        // Assert
        using var scope = new AssertionScope();
        result.Should().AllBeOfType<EntityDto>();
        var results = result.ToList();
        for (var i = 0; i < expected.Count; i++)
        {
            results[i].Id.Should().Be(expected[i].Id);
            results[i].TextRecordText.Should().Be(expected[i].TextRecord.Text);
        }
    }

    [Test]
    public async Task GetPagedList_WithPredicate_ReturnsCorrectPagedResults()
    {
        // Arrange
        var paging = new PaginatedRequest(1, 10, "_");
        var entity = TestData[0];

        // Act
        var result = await Repository.GetPagedListAsync<EntityDto>(e => e.Id == entity.Id, paging, Mapper!);

        // Assert
        using var scope = new AssertionScope();
        result.First().Should().BeOfType<EntityDto>();
        result.First().Id.Should().Be(entity.Id);
        result.First().TextRecordText.Should().Be(entity.TextRecord.Text);
    }

    [Test]
    public async Task WhenNoItemsExist_ReturnsEmptyList()
    {
        // Arrange
        Repository.Items.Clear();
        var paging = new PaginatedRequest(1, 10, "_");

        // Act
        var result = await Repository.GetPagedListAsync<EntityDto>(paging, Mapper!);

        // Assert
        result.Should().BeEmpty();
    }

    [Test]
    public async Task WhenPagedBeyondExistingItems_ReturnsEmptyList()
    {
        var paging = new PaginatedRequest(2, TestData.Count, "_");

        var result = await Repository.GetPagedListAsync<EntityDto>(paging, Mapper!);

        result.Should().BeEmpty();
    }

    [Test]
    public async Task GivenAscSorting_ReturnsAscSortedList()
    {
        // Arrange
        var items = TestData.OrderBy(entity => entity.Note).ToList();
        var paging = new PaginatedRequest(1, items.Count, "Note");

        // Act
        var result = await Repository.GetPagedListAsync<EntityDto>(paging, Mapper!);

        // Assert
        using var scope = new AssertionScope();
        result.Should().AllBeOfType<EntityDto>();
        var results = result.ToList();
        for (var i = 0; i < TestData.Count; i++)
        {
            results[i].Id.Should().Be(items[i].Id);
            results[i].TextRecordText.Should().Be(items[i].TextRecord.Text);
        }
    }

    [Test]
    public async Task GivenDescSorting_ReturnsDescSortedList()
    {
        // Arrange
        var items = TestData.OrderByDescending(entity => entity.Note).ToList();
        var paging = new PaginatedRequest(1, items.Count, "Note desc");

        // Act
        var result = await Repository.GetPagedListAsync<EntityDto>(paging, Mapper!);

        // Assert
        using var scope = new AssertionScope();
        result.Should().AllBeOfType<EntityDto>();
        var results = result.ToList();
        for (var i = 0; i < TestData.Count; i++)
        {
            results[i].Id.Should().Be(items[i].Id);
            results[i].TextRecordText.Should().Be(items[i].TextRecord.Text);
        }
    }
}
