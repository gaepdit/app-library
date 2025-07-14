namespace AppLibrary.Tests.EfRepositoryTests.IncludePropertiesTests;

public class FindIncludeProperties : TestsBase
{
    [Test]
    public async Task Find_WhenEntityDoesNotExist_ReturnsNull()
    {
        // Arrange
        var id = Guid.NewGuid();

        // Act
        var result = await Repository.FindAsync(id, includeProperties: []);

        // Assert
        result.Should().BeNull();
    }

    [Test]
    public async Task WhenRequestingProperty_ReturnsEntityWithProperty()
    {
        // Arrange
        var expected = TestData[0];

        // Act
        var result = await Repository.FindAsync(expected.Id, includeProperties: [TextRecordsName]);

        // Assert
        result!.TextRecords.Should().HaveCount(expected.TextRecords.Count);
    }

    [Test]
    public async Task WhenNotRequestingProperty_ReturnsEntityWithoutProperty()
    {
        // Arrange
        var expected = TestData[0];

        // Act
        var result = await Repository.FindAsync(expected.Id, includeProperties: []);

        // Assert
        result!.TextRecords.Should().BeEmpty();
    }
}
