using AppLibrary.Tests.RepositoryTestHelpers;

namespace AppLibrary.Tests.EfRepositoryTests.NamedEntityProjectToTests;

public class FindByNameAndProject : TestsBase
{
    [Test]
    public async Task FindByName_WhenEntityExists_ReturnsEntityAsDto()
    {
        // Arrange
        var entity = TestDataWithChildProperties[0];

        // Act
        var result =
            await Repository.FindByNameAsync<NamedEntityWithChildPropertyDto>(
                entity.Name, Mapper!);

        // Assert
        using var scope = new AssertionScope();
        result.Should().BeOfType<NamedEntityWithChildPropertyDto>();
        result.Id.Should().Be(entity.Id);
        result.TextRecordText.Should().Be(entity.TextRecord!.Text);
    }

    [Test]
    public async Task FindByName_WhenEntityDoesNotExist_ReturnsNull()
    {
        var result =
            await Repository.FindByNameAsync<NamedEntityWithChildPropertyDto>("xxx",
                Mapper!);

        result.Should().BeNull();
    }
}
