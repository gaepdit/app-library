using GaEpd.AppLibrary.Pagination;

namespace AppLibrary.Tests.EfRepositoryTests.IncludePropertiesTests;

public class GetPagedListIncludeProperties : TestsBase
{
    [Test]
    public async Task GetPagedList_WithNoIncludedProperties_ReturnsWithoutProperties()
    {
        // Arrange
        var paging = new PaginatedRequest(1, 10, "_");

        // Act
        var results = await Repository.GetPagedListAsync(paging, includeProperties: []);

        // Assert
        results.Should().AllSatisfy(entity => entity.TextRecords.Should().BeEmpty());
    }

    [Test]
    public async Task GetPagedList_WithIncludedProperties_ReturnsWithProperties()
    {
        // Arrange
        var paging = new PaginatedRequest(1, 10, "_");

        // Act
        var results = await Repository.GetPagedListAsync(paging, includeProperties: [TextRecordsName]);

        // Assert
        results.Should().AllSatisfy(entity => entity.TextRecords.Should().NotBeEmpty());
    }

    [Test]
    public async Task GetPagedList_WithPredicate_WithNoIncludedProperties_ReturnsWithoutProperties()
    {
        // Arrange
        var paging = new PaginatedRequest(1, 10, "_");

        // Act
        var results =
            await Repository.GetPagedListAsync(e => e.Note == TestData[0].Note, paging, includeProperties: []);

        // Assert
        results.Should().AllSatisfy(entity => entity.TextRecords.Should().BeEmpty());
    }

    [Test]
    public async Task GetPagedList_WithPredicate_WithIncludedProperties_ReturnsWithProperties()
    {
        // Arrange
        var paging = new PaginatedRequest(1, 10, "_");

        // Act
        var results = await Repository.GetPagedListAsync(e => e.Note == TestData[0].Note, paging,
            includeProperties: [TextRecordsName]);

        // Assert
        results.Should().AllSatisfy(entity => entity.TextRecords.Should().NotBeEmpty());
    }
}
