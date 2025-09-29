using AppLibrary.Tests.RepositoryTestHelpers;
using GaEpd.AppLibrary.Pagination;

namespace AppLibrary.Tests.LocalRepositoryTests.ProjectDerivedEntityTests;

public class GetPagedListAndProject : TestsBase
{
    [Test]
    public async Task GetPagedList_ReturnsCorrectPagedResults()
    {
        // Arrange
        var paging = new PaginatedRequest(2, 1, nameof(TestEntity.Id));
        var expected = TestData.Where(e => e is EntityWithChildProperty)
            .OrderBy(entity => entity.Id).Skip(paging.Skip).Take(paging.Take).ToList();

        // Act
        var result = await Repository.GetPagedListAsync<EntityDto, EntityWithChildProperty>(paging, Mapper!);

        // Assert
        using var scope = new AssertionScope();
        result.Should().AllBeOfType<EntityDto>();
        result.Should().BeEquivalentTo(expected);
    }

    [Test]
    public async Task GetPagedList_WithPredicate_ReturnsCorrectPagedResults()
    {
        // Arrange
        var paging = new PaginatedRequest(1, 10, nameof(EntityWithChildProperty.Id));
        var expected = (EntityWithChildProperty)TestData.First(e => e is EntityWithChildProperty);

        // Act
        var result = await Repository.GetPagedListAsync<EntityDto, EntityWithChildProperty>(e => e.Id == expected.Id,
            paging, Mapper!);

        // Assert
        result.Single().Should().BeEquivalentTo(Mapper!.Map<EntityDto>(expected));
    }

    [Test]
    public async Task WhenNoItemsExist_ReturnsEmptyList()
    {
        // Arrange
        Repository.Items.Clear();
        var paging = new PaginatedRequest(1, 10, nameof(EntityDto.Id));

        // Act
        var result = await Repository.GetPagedListAsync<EntityDto, EntityWithChildProperty>(paging, Mapper!);

        // Assert
        result.Should().BeEmpty();
    }

    [Test]
    public async Task WhenPagedBeyondExistingItems_ReturnsEmptyList()
    {
        // Arrange
        var paging = new PaginatedRequest(2, TestData.Count, nameof(EntityDto.Id));

        // Act
        var result = await Repository.GetPagedListAsync<EntityDto, EntityWithChildProperty>(paging, Mapper!);

        // Assert
        result.Should().BeEmpty();
    }

    [Test]
    public async Task GivenAscSorting_ReturnsAscSortedList()
    {
        // Arrange
        var expected = TestData.Where(e => e is EntityWithChildProperty)
            .OrderBy(entity => entity.Note).ToList();
        var paging = new PaginatedRequest(1, expected.Count, nameof(TestEntity.Note));

        // Act
        var result = await Repository.GetPagedListAsync<EntityDto, EntityWithChildProperty>(paging, Mapper!);

        // Assert
        using var scope = new AssertionScope();
        result.Should().AllBeOfType<EntityDto>();
        result.Should().BeEquivalentTo(expected);
    }

    [Test]
    public async Task GivenDescSorting_ReturnsDescSortedList()
    {
        // Arrange
        var expected = TestData.Where(e => e is EntityWithChildProperty)
            .OrderByDescending(entity => entity.Note).ToList();
        var paging = new PaginatedRequest(1, expected.Count, $"{nameof(EntityDto.Note)} desc");

        // Act
        var result = await Repository.GetPagedListAsync<EntityDto, EntityWithChildProperty>(paging, Mapper!);

        // Assert
        using var scope = new AssertionScope();
        result.Should().AllBeOfType<EntityDto>();
        result.Should().BeEquivalentTo(expected);
    }
}
