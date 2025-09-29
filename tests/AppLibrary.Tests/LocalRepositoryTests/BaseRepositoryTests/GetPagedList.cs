using AppLibrary.Tests.RepositoryTestHelpers;
using GaEpd.AppLibrary.Pagination;

namespace AppLibrary.Tests.LocalRepositoryTests.BaseRepositoryTests;

public class GetPagedList : TestsBase
{
    [Test]
    public async Task GetPagedList_ReturnsCorrectPagedResults()
    {
        var paging = new PaginatedRequest(2, 1, nameof(TestEntity.Id));
        var expectedResults = Repository.Items.OrderBy(e => e.Id).Skip(paging.Skip).Take(paging.Take).ToList();

        var results = await Repository.GetPagedListAsync(paging);

        results.Should().BeEquivalentTo(expectedResults);
    }

    [Test]
    public async Task GetPagedList_WithPredicate_ReturnsCorrectPagedResults()
    {
        // Assuming this is the correct selection based on your predicate.
        var paging = new PaginatedRequest(1, 1, nameof(TestEntity.Id));
        var selectedItems = Repository.Items.OrderBy(e => e.Id).Skip(1).ToList();
        var expectedResults = selectedItems.Skip(paging.Skip).Take(paging.Take).ToList();

        var results = await Repository.GetPagedListAsync(e => e.Id == selectedItems[0].Id, paging);

        results.Should().BeEquivalentTo(expectedResults);
    }

    [Test]
    public async Task WhenNoItemsExist_ReturnsEmptyList()
    {
        Repository.Items.Clear();
        var paging = new PaginatedRequest(1, 1, nameof(TestEntity.Id));

        var result = await Repository.GetPagedListAsync(paging);

        result.Should().BeEmpty();
    }

    [Test]
    public async Task WhenPagedBeyondExistingItems_ReturnsEmptyList()
    {
        var paging = new PaginatedRequest(2, Repository.Items.Count, nameof(TestEntity.Id));

        var result = await Repository.GetPagedListAsync(paging);

        result.Should().BeEmpty();
    }

    [Test]
    public async Task GivenAscSorting_ReturnsAscSortedList()
    {
        // Arrange
        var expected = Repository.Items.OrderBy(entity => entity.Note).ToList();
        var paging = new PaginatedRequest(1, expected.Count, nameof(TestEntity.Note));

        // Act
        var result = await Repository.GetPagedListAsync(paging);

        // Assert
        result.Should().BeEquivalentTo(expected, options => options.WithStrictOrdering());
    }

    [Test]
    public async Task GivenDescSorting_ReturnsDescSortedList()
    {
        // Arrange
        var expected = Repository.Items.OrderByDescending(entity => entity.Note).ToList();
        var paging = new PaginatedRequest(1, expected.Count, $"{nameof(TestEntity.Note)} desc");

        // Act
        var result = await Repository.GetPagedListAsync(paging);

        // Assert
        result.Should().BeEquivalentTo(expected, options => options.WithStrictOrdering());
    }
}
