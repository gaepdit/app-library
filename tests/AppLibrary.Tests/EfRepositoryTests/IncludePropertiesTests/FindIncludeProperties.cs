﻿using AppLibrary.Tests.TestEntities;

namespace AppLibrary.Tests.EfRepositoryTests.IncludePropertiesTests;

public class FindIncludeProperties : NavigationPropertiesTestBase
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
        var expected = NavigationPropertyEntities[0];

        // Act
        const string textRecordsName = nameof(TestEntityWithNavigationProperties.TextRecords);
        var result = await Repository.FindAsync(expected.Id, includeProperties: [textRecordsName]);

        // Assert
        using var scope = new AssertionScope();
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(expected);
        result.TextRecords.Should().HaveCount(expected.TextRecords.Count);
    }

    [Test]
    public async Task WhenNotRequestingProperty_ReturnsEntityWithoutProperty()
    {
        // Arrange
        var expected = NavigationPropertyEntities[0];

        // Act
        var result = await Repository.FindAsync(expected.Id, includeProperties: []);

        // Assert
        using var scope = new AssertionScope();
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(expected, options => options.Excluding(entity => entity.TextRecords));
        result.TextRecords.Should().BeEmpty();
    }
}
