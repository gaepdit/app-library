using AppLibrary.Tests.RepositoryTestHelpers;
using GaEpd.AppLibrary.Domain.Repositories;

namespace AppLibrary.Tests.EfRepositoryTests.IncludePropertiesTests;

public class GetIncludeProperties : TestsBase
{
    [Test]
    public void Get_WhenEntityDoesNotExist_ThrowsException()
    {
        // Arrange
        var id = Guid.NewGuid();

        // Act
        var func = async () => await Repository.GetAsync(id, includeProperties: []);

        // Assert
        func.Should().ThrowAsync<EntityNotFoundException<TestEntity>>()
            .WithMessage($"Entity not found. Entity type: {typeof(TestEntity).FullName}, id: {id}");
    }

    [Test]
    public async Task WhenRequestingProperty_ReturnsEntityWithProperty()
    {
        // Arrange
        var expected = TestData[0];

        // Act
        var result = await Repository.GetAsync(expected.Id, includeProperties: [TextRecordsName]);

        // Assert
        result.TextRecords.Should().HaveCount(expected.TextRecords.Count);
    }

    [Test]
    public async Task WhenNotRequestingProperty_ReturnsEntityWithoutProperty()
    {
        // Arrange
        var expected = TestData[0];

        // Act
        var result = await Repository.GetAsync(expected.Id, includeProperties: []);

        // Assert
        result.TextRecords.Should().BeEmpty();
    }
}
