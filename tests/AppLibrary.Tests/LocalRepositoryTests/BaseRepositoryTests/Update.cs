using AppLibrary.Tests.RepositoryTestHelpers;
using GaEpd.AppLibrary.Domain.Repositories;

namespace AppLibrary.Tests.LocalRepositoryTests.BaseRepositoryTests;

public class Update : TestsBase
{
    [Test]
    public async Task Update_UpdateExistingItem_ShouldReflectChanges()
    {
        var originalEntity = Repository.Items.First();
        var newEntityWithSameId = new TestEntity { Id = originalEntity.Id, Note = "Xyz" };

        await Repository.UpdateAsync(newEntityWithSameId);

        var result = await Repository.GetAsync(newEntityWithSameId.Id);

        using var scope = new AssertionScope();
        result.Should().BeEquivalentTo(newEntityWithSameId);
        Repository.Items.Should().NotContain(originalEntity);
    }

    [Test]
    public void Update_WhenItemDoesNotExist_Throws()
    {
        var item = new TestEntity { Id = Guid.NewGuid() };

        var func = async () => await Repository.UpdateAsync(item);

        func.Should().ThrowAsync<EntityNotFoundException<TestEntity>>()
            .WithMessage($"Entity not found. Entity type: {typeof(TestEntity).FullName}, id: {item.Id}");
    }
}
