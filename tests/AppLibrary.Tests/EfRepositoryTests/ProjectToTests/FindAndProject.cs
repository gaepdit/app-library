using AppLibrary.Tests.RepositoryTestHelpers;

namespace AppLibrary.Tests.EfRepositoryTests.ProjectToTests;

public class FindAndProject : TestsBase
{
    [Test]
    public async Task Find_WhenEntityExists_ReturnsDto()
    {
        // Arrange
        var entity = TestData[0];

        // Act
        var result = await Repository.FindAsync<EntityDto>(entity.Id, Mapper!);

        // Assert
        using var scope = new AssertionScope();
        result.Should().BeOfType<EntityDto>();
        result.Id.Should().Be(entity.Id);
        result.TextRecordText.Should().Be(entity.TextRecord.Text);
    }

    [Test]
    public async Task Find_WhenEntityDoesNotExist_ReturnsNull()
    {
        var id = Guid.NewGuid();

        var result = await Repository.FindAsync<EntityDto>(id, Mapper!);

        result.Should().BeNull();
    }

    [Test]
    public async Task Find_UsingPredicate_WhenEntityExists_ReturnsDto()
    {
        // Arrange
        var entity = TestData[0];

        // Act
        var result = await Repository.FindAsync<EntityDto>(e => e.Id == entity.Id, Mapper!);

        // Assert
        using var scope = new AssertionScope();
        result.Should().BeOfType<EntityDto>();
        result.Id.Should().Be(entity.Id);
        result.TextRecordText.Should().Be(entity.TextRecord.Text);
    }

    [Test]
    public async Task Find_UsingPredicate_WhenEntityDoesNotExist_ReturnsNull()
    {
        var id = Guid.NewGuid();

        var result = await Repository.FindAsync<EntityDto>(e => e.Id == id, Mapper!);

        result.Should().BeNull();
    }
}
