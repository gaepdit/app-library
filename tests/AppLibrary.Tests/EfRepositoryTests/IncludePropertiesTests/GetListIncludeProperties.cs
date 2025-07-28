namespace AppLibrary.Tests.EfRepositoryTests.IncludePropertiesTests;

public class GetListIncludeProperties : TestsBase
{
    [Test]
    public async Task GetPagedList_WithNoIncludedProperties_ReturnsWithoutProperties()
    {
        // Act
        var results = await Repository.GetListAsync(includeProperties: []);

        // Assert
        results.Should().AllSatisfy(entity => entity.TextRecords.Should().BeEmpty());
    }

    [Test]
    public async Task GetPagedList_WithIncludedProperties_ReturnsWithProperties()
    {
        // Act
        var results = await Repository.GetListAsync(includeProperties: [TextRecordsName]);

        // Assert
        results.Should().AllSatisfy(entity => entity.TextRecords.Should().NotBeEmpty());
    }

    [Test]
    public async Task GetPagedList_WithPredicate_WithNoIncludedProperties_ReturnsWithoutProperties()
    {
        // Act
        var results = await Repository.GetListAsync(e => e.Note == TestData[0].Note, includeProperties: []);

        // Assert
        results.Should().AllSatisfy(entity => entity.TextRecords.Should().BeEmpty());
    }

    [Test]
    public async Task GetPagedList_WithPredicate_WithIncludedProperties_ReturnsWithProperties()
    {
        // Act
        var results =
            await Repository.GetListAsync(e => e.Note == TestData[0].Note, includeProperties: [TextRecordsName]);

        // Assert
        results.Should().AllSatisfy(entity => entity.TextRecords.Should().NotBeEmpty());
    }
}
