using AppLibrary.Tests.TestEntities;
using Microsoft.EntityFrameworkCore;

namespace AppLibrary.Tests.EfRepositoryTests.IncludePropertiesTests;

public class GetListIncludeProperties : NavigationPropertiesTestBase
{
    private const string TextRecordsName = nameof(TestEntityWithNavigationProperties.TextRecords);

    [Test]
    public async Task GetPagedList_WithNoIncludedProperties_ReturnsWithoutProperties()
    {
        // Arrange
        var expectedResults = await Repository.Context.Set<TestEntityWithNavigationProperties>()
            .Include(entity => entity.TextRecords).ToListAsync();

        // Act
        var results = await Repository.GetListAsync(includeProperties: []);

        // Assert
        using var scope = new AssertionScope();
        results.Should().NotBeEquivalentTo(expectedResults);
        results.Should().BeEquivalentTo(expectedResults, options => options.Excluding(entity => entity.TextRecords));
    }

    [Test]
    public async Task GetPagedList_WithIncludedProperties_ReturnsWithProperties()
    {
        // Arrange
        var expectedResults = await Repository.Context.Set<TestEntityWithNavigationProperties>()
            .Include(entity => entity.TextRecords).ToListAsync();

        // Act
        var results = await Repository.GetListAsync(includeProperties: [TextRecordsName]);

        // Assert
        results.Should().BeEquivalentTo(expectedResults);
    }

    [Test]
    public async Task GetPagedList_WithPredicate_WithNoIncludedProperties_ReturnsWithoutProperties()
    {
        // Arrange
        var excludedName = NavigationPropertyEntities[0].Name;
        var expectedResults = await Repository.Context.Set<TestEntityWithNavigationProperties>()
            .Include(entity => entity.TextRecords)
            .Where(entity => entity.Name != excludedName).ToListAsync();

        // Act
        var results = await Repository.GetListAsync(e => e.Name != excludedName, includeProperties: []);

        // Assert
        using var scope = new AssertionScope();
        results.Should().NotBeEquivalentTo(expectedResults);
        results.Should().BeEquivalentTo(expectedResults, options => options.Excluding(entity => entity.TextRecords));
    }

    [Test]
    public async Task GetPagedList_WithPredicate_WithIncludedProperties_ReturnsWithProperties()
    {
        // Arrange
        var excludedName = NavigationPropertyEntities[0].Name;
        var expectedResults = await Repository.Context.Set<TestEntityWithNavigationProperties>()
            .Include(entity => entity.TextRecords)
            .Where(entity => entity.Name != excludedName).ToListAsync();

        // Act
        var results = await Repository.GetListAsync(e => e.Name != excludedName, includeProperties: [TextRecordsName]);

        // Assert
        results.Should().BeEquivalentTo(expectedResults);
    }
}
