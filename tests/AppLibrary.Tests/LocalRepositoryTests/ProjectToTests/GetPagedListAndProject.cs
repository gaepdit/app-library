using AppLibrary.Tests.RepositoryTestHelpers;
using GaEpd.AppLibrary.Pagination;

namespace AppLibrary.Tests.LocalRepositoryTests.ProjectToTests;

public class GetPagedListAndProject : TestsBase
{
    [Test]
    public async Task GetPagedList_ReturnsCorrectPagedResults()
    {
        // Arrange
        var paging = new PaginatedRequest(2, 1, nameof(EntityWithChildProperty.Id));
        var expected = TestData.OrderBy(entity => entity.Id).Skip(paging.Skip).Take(paging.Take).ToList();

        // Act
        var result = await Repository.GetPagedListAsync<EntityDto>(paging, Mapper!);

        // Assert
        using var scope = new AssertionScope();
        result.Should().AllBeOfType<EntityDto>();
        result.Should().BeEquivalentTo(Mapper!.Map<List<EntityDto>>(expected));
    }

    [Test]
    public async Task GetPagedList_WithPredicate_ReturnsCorrectPagedResults()
    {
        // Arrange
        var paging = new PaginatedRequest(1, 10, nameof(EntityWithChildProperty.Id));
        var expected = TestData[0];

        // Act
        var result = await Repository.GetPagedListAsync<EntityDto>(e => e.Id == expected.Id, paging, Mapper!);

        // Assert
        var entityDto = result.Single();
        using var scope = new AssertionScope();
        entityDto.Should().BeEquivalentTo(expected, options => options.Excluding(e => e.TextRecord));
        entityDto.TextRecordText.Should().Be(expected.TextRecord.Text);
    }

    [Test]
    public async Task WhenNoItemsExist_ReturnsEmptyList()
    {
        // Arrange
        Repository.Items.Clear();
        var paging = new PaginatedRequest(1, 10, nameof(EntityDto.Id));

        // Act
        var result = await Repository.GetPagedListAsync<EntityDto>(paging, Mapper!);

        // Assert
        result.Should().BeEmpty();
    }

    [Test]
    public async Task WhenPagedBeyondExistingItems_ReturnsEmptyList()
    {
        // Arrange
        var paging = new PaginatedRequest(2, TestData.Count, nameof(EntityDto.Id));

        // Act
        var result = await Repository.GetPagedListAsync<EntityDto>(paging, Mapper!);

        // Assert
        result.Should().BeEmpty();
    }

    [Test]
    public async Task GivenAscSorting_ReturnsAscSortedList()
    {
        // Arrange
        var expected = TestData.OrderBy(entity => entity.Note).ToList();
        var paging = new PaginatedRequest(1, expected.Count, nameof(EntityWithChildProperty.Note));

        // Act
        var result = await Repository.GetPagedListAsync<EntityDto>(paging, Mapper!);

        // Assert
        using var scope = new AssertionScope();
        result.Should().AllBeOfType<EntityDto>();
        result.Should().BeEquivalentTo(Mapper!.Map<List<EntityDto>>(expected));
    }

    [Test]
    public async Task GivenDescSorting_ReturnsDescSortedList()
    {
        // Arrange
        var expected = TestData.OrderByDescending(entity => entity.Note).ToList();
        var paging = new PaginatedRequest(1, expected.Count, $"{nameof(EntityDto.Note)} desc");

        // Act
        var result = await Repository.GetPagedListAsync<EntityDto>(paging, Mapper!);

        // Assert
        using var scope = new AssertionScope();
        result.Should().AllBeOfType<EntityDto>();
        result.Should().BeEquivalentTo(Mapper!.Map<List<EntityDto>>(expected));
    }
}
