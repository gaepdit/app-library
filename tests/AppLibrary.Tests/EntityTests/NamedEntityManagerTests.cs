﻿using AppLibrary.Tests.TestEntities;
using GaEpd.AppLibrary.Domain.Entities;
using GaEpd.AppLibrary.Domain.Repositories;
using NSubstitute;

namespace AppLibrary.Tests.EntityTests;

public class NamedEntityManagerTests
{
    private INamedEntityRepository<TestNamedEntity> _repositoryMock;
    private TestNamedEntityManager _manager;

    [SetUp]
    public void Setup()
    {
        _repositoryMock = Substitute.For<INamedEntityRepository<TestNamedEntity>>();
        _manager = new TestNamedEntityManager(_repositoryMock);
    }

    [TearDown]
    public async Task TearDown() => await _repositoryMock.DisposeAsync();

    [Test]
    public async Task Create_ShouldReturnEntityWithGivenName()
    {
        var result = await _manager.CreateAsync("Test");

        result.Name.Should().Be("Test");
    }

    [Test]
    public async Task ChangeName_ShouldChangeEntityName()
    {
        var entity = new TestNamedEntity(Guid.NewGuid(), "OldName");

        await _manager.ChangeNameAsync(entity, "NewName");

        entity.Name.Should().Be("NewName");
    }

    [Test]
    public void ThrowIfDuplicateName_ShouldThrowDuplicateNameException()
    {
        _repositoryMock.FindByNameAsync("Duplicate", Arg.Any<CancellationToken>())
            .Returns(new TestNamedEntity(Guid.NewGuid(), "Duplicate"));

        var func = async () => await _manager.CreateAsync("Duplicate");

        func.Should().ThrowAsync<DuplicateNameException>().WithMessage("Name 'Duplicate' is already in use.");
    }
}
