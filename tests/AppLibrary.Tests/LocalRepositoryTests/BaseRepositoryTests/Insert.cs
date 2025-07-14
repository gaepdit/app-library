using AppLibrary.Tests.RepositoryTestHelpers;

namespace AppLibrary.Tests.LocalRepositoryTests.BaseRepositoryTests;

public class Insert : TestsBase
{
    [Test]
    public async Task Insert_AddNewItem_ShouldIncreaseCountByOne()
    {
        var initialCount = Repository.Items.Count;
        var entity = new TestEntity { Id = Guid.NewGuid() };

        await Repository.InsertAsync(entity);

        Repository.Items.Should().HaveCount(initialCount + 1);
    }

    [Test]
    public async Task Insert_AddNewItem_ShouldBeAbleToRetrieveNewItem()
    {
        var entity = new TestEntity { Id = Guid.NewGuid() };

        await Repository.InsertAsync(entity);
        var result = await Repository.GetAsync(entity.Id);

        result.Should().BeEquivalentTo(entity);
    }
}
