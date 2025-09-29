using AppLibrary.Tests.RepositoryTestHelpers;

namespace AppLibrary.Tests.EfRepositoryTests.ProjectDerivedEntityTests;

public class FindAndProject : TestsBase
{
    [Test]
    public async Task Find_WhenEntityExists_ReturnsDto()
    {
        // Arrange
        var entity = (EntityWithChildProperty)TestData.First(e => e is EntityWithChildProperty);

        // Act
        var result = await Repository.FindAsync<EntityDto, EntityWithChildProperty>(entity.Id, Mapper!);

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

        var result = await Repository.FindAsync<EntityDto, EntityWithChildProperty>(id, Mapper!);

        result.Should().BeNull();
    }

    [Test]
    public async Task Find_UsingPredicate_WhenEntityExists_ReturnsDto()
    {
        // Arrange
        var entity = (EntityWithChildProperty)TestData.First(e => e is EntityWithChildProperty);

        // Act
        var result = await Repository.FindAsync<EntityDto, EntityWithChildProperty>(e => e.Id == entity.Id, Mapper!);

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

        var result = await Repository.FindAsync<EntityDto, EntityWithChildProperty>(e => e.Id == id, Mapper!);

        result.Should().BeNull();
    }
}
