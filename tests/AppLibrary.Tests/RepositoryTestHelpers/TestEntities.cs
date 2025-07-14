using AutoMapper;
using GaEpd.AppLibrary.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace AppLibrary.Tests.RepositoryTestHelpers;

public class TestEntity : IEntity
{
    public Guid Id { get; init; }
    [StringLength(7)] public string Note { get; init; } = string.Empty;
}

public class EntityWithNavigationProperty : IEntity
{
    public Guid Id { get; init; }
    [StringLength(7)] public string Note { get; init; } = null!;
    public List<TextRecord> TextRecords { get; } = [];
}

public class EntityWithChildProperty : IEntity
{
    public Guid Id { get; init; }
    [StringLength(7)] public string Note { get; init; } = string.Empty;
    public TextRecord TextRecord { get; init; } = null!;
}

public class TestNamedEntity : StandardNamedEntity
{
    public override int MinNameLength => 4;
    public override int MaxNameLength => 9;

    public TestNamedEntity() { }
    public TestNamedEntity(Guid id, string name) : base(id, name) { }
}

public class NamedEntityWithChildProperty : StandardNamedEntity
{
    public override int MinNameLength => 4;
    public override int MaxNameLength => 9;

    public NamedEntityWithChildProperty() { }
    public NamedEntityWithChildProperty(Guid id, string name) : base(id, name) { }

    public TextRecord? TextRecord { get; init; }
}

public record TextRecord
{
    public Guid Id { get; init; }
    public string Text { get; init; } = string.Empty;
}

public class EntityDto
{
    public Guid Id { get; init; }
    public string Note { get; init; } = null!;
    public string TextRecordText { get; init; } = null!;
}

public class NamedEntityDto
{
    public Guid Id { get; init; }
    public string Name { get; init; } = null!;
}

public class NamedEntityWithChildPropertyDto
{
    public Guid Id { get; init; }
    public string Name { get; init; } = null!;
    public string TextRecordText { get; init; } = null!;
}

public class TestAutoMapperProfile : Profile
{
    public TestAutoMapperProfile()
    {
        CreateMap<EntityWithChildProperty, EntityDto>();
        CreateMap<TestNamedEntity, NamedEntityDto>();
        CreateMap<NamedEntityWithChildProperty, NamedEntityWithChildPropertyDto>();
    }
}
