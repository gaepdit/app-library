using AppLibrary.Tests.RepositoryTestHelpers;
using GaEpd.AppLibrary.Pagination;

namespace AppLibrary.Tests.EfRepositoryTests.ProjectToTests;

public class GetPagedListAndProject : TestsBase
{
    [Test]
    public async Task GetPagedList_ReturnsCorrectPagedResults()
    {
        // Arrange
        var paging = new PaginatedRequest(2, 1, nameof(EntityWithChildProperty.Id));
        var expected = TestData.OrderBy(e => e.Id).Skip(paging.Skip).Take(paging.Take).ToList();

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
        var paging = new PaginatedRequest(1, 10, nameof(EntityDto.Id));
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
        await Helper.ClearTableAsync<EntityWithChildProperty>();
        var paging = new PaginatedRequest(1, 10, nameof(EntityDto.Id));

        // Act
        var result = await Repository.GetPagedListAsync<EntityDto>(paging, Mapper!);

        // Assert
        result.Should().BeEmpty();
    }

    [Test]
    public async Task WhenPagedBeyondExistingItems_ReturnsEmptyList()
    {
        var paging = new PaginatedRequest(2, TestData.Count, nameof(EntityDto.Id));

        var result = await Repository.GetPagedListAsync<EntityDto>(paging, Mapper!);

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
