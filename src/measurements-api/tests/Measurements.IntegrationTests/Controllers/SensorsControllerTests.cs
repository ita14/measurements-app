using System.Net;
using System.Net.Http.Json;
using Bogus;
using FluentAssertions;

namespace Measurements.IntegrationTests.Controllers;

public class SensorsControllerTests : IntegrationTestBase
{
    public SensorsControllerTests(IntegrationTestWebApplicationFactory factory) : base(factory)
    {
    }

    [Fact]
    public async Task Get_ReturnsSensor()
    {
        // Arrange
        var sensor = new Faker<Measurements.Api.Domain.Entities.Sensor>()
            .RuleFor(x => x.Id, Guid.NewGuid().ToString)
            .RuleFor(x => x.Description, f => f.Lorem.Sentence())
            .Generate();

        await Persist(sensor);

        // Act
        var response = await Client.GetAsync($"/sensors/{sensor.Id}");

        // Assert
        response.Should().BeSuccessful();
        var returnedSensor = await response.Content.ReadFromJsonAsync<OpenApi.Measurements.Api.Sensor>();
        returnedSensor.Should().BeEquivalentTo(sensor);
    }

    [Fact]
    public async Task Post_CreatesNewSensor()
    {
        // Arrange
        var sensor = new Faker<OpenApi.Measurements.Api.Sensor>()
            .RuleFor(x => x.Id, Guid.NewGuid().ToString)
            .RuleFor(x => x.Description, f => f.Lorem.Sentence())
            .Generate();

        // Act
        var response = await Client.PostAsJsonAsync("/sensors", sensor);

        // Assert
        response.Should().BeSuccessful();
        var returnedSensor = await response.Content.ReadFromJsonAsync<OpenApi.Measurements.Api.Sensor>();
        returnedSensor.Should().BeEquivalentTo(sensor);
    }

    [Fact]
    public async Task Put_UpdatesSensor()
    {
        // Arrange
        var sensor = new Faker<Measurements.Api.Domain.Entities.Sensor>()
            .RuleFor(x => x.Id, Guid.NewGuid().ToString)
            .RuleFor(x => x.Description, f => f.Lorem.Sentence())
            .Generate();

        await Persist(sensor);

        var apiSensor = new OpenApi.Measurements.Api.Sensor
        {
            Id = sensor.Id,
            Description = "Updated"
        };

        // Act
        var response = await Client.PutAsJsonAsync($"/sensors/{sensor.Id}", apiSensor);

        // Assert
        response.Should().HaveStatusCode(HttpStatusCode.NoContent);
    }

    [Fact]
    public async Task Delete_RemovesSensor()
    {
        // Arrange
        var sensor = new Faker<Measurements.Api.Domain.Entities.Sensor>()
            .RuleFor(x => x.Id, Guid.NewGuid().ToString)
            .RuleFor(x => x.Description, f => f.Lorem.Sentence())
            .Generate();

        await Persist(sensor);

        // Act
        var response = await Client.DeleteAsync($"/sensors/{sensor.Id}");

        // Assert
        response.Should().HaveStatusCode(HttpStatusCode.NoContent);
        var deletedSensor = await Context.Sensors.FindAsync(sensor.Id);
        deletedSensor.Should().BeNull();
    }
}
