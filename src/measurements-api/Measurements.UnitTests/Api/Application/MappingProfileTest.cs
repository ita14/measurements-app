using AutoMapper;
using Measurements.Api;

namespace Measurements.UnitTests.Api.Application;

[TestClass]
public class MappingProfileTest
{
    [TestMethod]
    public void Verify_AutomapperProfile()
    {
        var configuration = new MapperConfiguration(cfg =>
            cfg.AddProfile(new ApiMappingProfile()));

        configuration.AssertConfigurationIsValid();
    }
}
