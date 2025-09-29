using AppLibrary.Tests.RepositoryTestHelpers;

namespace AppLibrary.Tests.EfRepositoryTests.BaseRepositoryTests;

public class GetListWithOrdering : TestsBase
{
    [Test]
    public async Task GetListAsc_ReturnsAllEntitiesInAscendingOrder()
    {
        var expected = Repository.Context.Set<TestEntity>().OrderBy(entity => entity.Note);

        var result = await Repository.GetListAsync(ordering: nameof(TestEntity.Note));

        result.Should().BeEquivalentTo(expected, options => options.WithStrictOrdering());
    }

    [Test]
    public async Task GetListDesc_ReturnsAllEntitiesInDescendingOrder()
    {
        var expected = Repository.Context.Set<TestEntity>().OrderByDescending(entity => entity.Note);

        var result = await Repository.GetListAsync(ordering: $"{nameof(TestEntity.Note)} desc");

        result.Should().BeEquivalentTo(expected, options => options.WithStrictOrdering());
    }

    [Test]
    public async Task WhenNoItemsExist_ReturnsEmptyList()
    {
        await Helper.ClearTableAsync<TestEntity>();

        var result = await Repository.GetListAsync(ordering: nameof(TestEntity.Note));

        result.Should().BeEmpty();
    }

    [Test]
    public async Task GetListAsc_UsingPredicate_ReturnsCorrectEntitiesInAscendingOrder()
    {
        var skipId = Repository.Context.Set<TestEntity>().First().Id;
        var expected = Repository.Context.Set<TestEntity>().Where(entity => entity.Id != skipId)
            .OrderBy(entity => entity.Note);

        var result = await Repository.GetListAsync(entity => entity.Id != skipId, ordering: nameof(TestEntity.Note));

        result.Should().BeEquivalentTo(expected);
    }

    [Test]
    public async Task GetListDesc_UsingPredicate_ReturnsCorrectEntitiesInDescendingOrder()
    {
        var skipId = Repository.Context.Set<TestEntity>().First().Id;
        var expected = Repository.Context.Set<TestEntity>().Where(entity => entity.Id != skipId)
            .OrderByDescending(entity => entity.Note);

        var result =
            await Repository.GetListAsync(entity => entity.Id != skipId, ordering: $"{nameof(TestEntity.Note)} desc");

        result.Should().BeEquivalentTo(expected);
    }

    [Test]
    public async Task GetList_UsingPredicate_WhenNoItemsMatch_ReturnsEmptyList()
    {
        var id = Guid.NewGuid();

        var result = await Repository.GetListAsync(entity => entity.Id == id, ordering: nameof(TestEntity.Note));

        result.Should().BeEmpty();
    }
}
