using AutoMapper;
using Measurements.Api.Application;

namespace Measurements.UnitTests.Api.Application;

[TestClass]
public class MappingProfileTest
{
    [TestMethod]
    public void Verify_AutomapperProfile()
    {
        var configuration = new MapperConfiguration(cfg =>
            cfg.AddProfile(new MappingProfile()));

        configuration.AssertConfigurationIsValid();
    }
}
