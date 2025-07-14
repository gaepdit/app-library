using AutoMapper;

namespace AppLibrary.Tests.RepositoryTestHelpers;

public class AutoMapperConfiguration
{
    [Test]
    public void MappingConfigurationsAreValid()
    {
        new MapperConfiguration(configure: configuration => configuration.AddProfile(new TestAutoMapperProfile()))
            .AssertConfigurationIsValid();
    }
}
