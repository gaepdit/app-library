﻿using AppLibrary.Tests.EntityHelpers;

namespace AppLibrary.Tests.EfRepositoryTests;

public class Count : EfRepositoryTestBase
{
    [Test]
    public async Task CountAsync_WithoutPredicate_ReturnsCorrectCount()
    {
        var result = await Repository.CountAsync(e => true);

        result.Should().Be(Repository.Context.Set<DerivedEntity>().Count());
    }

    [Test]
    public async Task CountAsync_WithoutPredicate_WhenNoItemsExist_ReturnsZero()
    {
        await Helper.ClearTableAsync<DerivedEntity>();

        var result = await Repository.CountAsync(e => true);

        result.Should().Be(0);
    }

    [Test]
    public async Task CountAsync_WithPredicate_ReturnsCorrectCount()
    {
        // Assuming this is the count you are expecting from your predicate.
        var selectedItemsCount = Repository.Context.Set<DerivedEntity>().Count() - 1;

        var result = await Repository.CountAsync(e => e.Id != Repository.Context.Set<DerivedEntity>().First().Id);

        result.Should().Be(selectedItemsCount);
    }

    [Test]
    public async Task CountAsync_WithPredicateThatDoesNotMatchAnyEntity_ReturnsZero()
    {
        var id = Guid.NewGuid();

        var result = await Repository.CountAsync(e => e.Id == id);

        result.Should().Be(0);
    }
}
