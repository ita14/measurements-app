using System.Net;
using System.Net.Http.Json;
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
        var sensor = new Measurements.Api.Domain.Entities.Sensor { /* initialize sensor properties here */ };
        sensor = await Persist(sensor);

        // Act
        var response = await Client.GetAsync($"/api/sensors/{sensor.Id}");

        // Assert
        response.EnsureSuccessStatusCode();
        var returnedSensor = await response.Content.ReadFromJsonAsync<OpenApi.Measurements.Api.Sensor>();
        returnedSensor.Id.Should().Be(sensor.Id);
        // Add more assertions for other sensor properties
    }

    [Fact]
    public async Task Post_CreatesNewSensor()
    {
        // Arrange
        var sensor = new OpenApi.Measurements.Api.Sensor { /* initialize sensor properties here */ };

        // Act
        var response = await Client.PostAsJsonAsync("/api/sensors", sensor);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Created);
        var returnedSensor = await response.Content.ReadFromJsonAsync<OpenApi.Measurements.Api.Sensor>();
        returnedSensor.Id.Should().NotBeNull();
        // Add more assertions for other sensor properties
    }

    [Fact]
    public async Task Put_UpdatesSensor()
    {
        // Arrange
        var sensor = new Measurements.Api.Domain.Entities.Sensor
        {
            Id = "id",
            Description = "description"
        };
        sensor = await Persist(sensor);

        var apiSensor = new OpenApi.Measurements.Api.Sensor
        {
            Id = "id",
            Description = "Updated"
        };

        // Act
        var response = await Client.PutAsJsonAsync($"/api/sensors/{sensor.Id}", apiSensor);

        // Assert
        response.EnsureSuccessStatusCode();
        var returnedSensor = await response.Content.ReadFromJsonAsync<OpenApi.Measurements.Api.Sensor>();
        returnedSensor.Description.Should().Be(apiSensor.Description);
    }

    [Fact]
    public async Task Delete_RemovesSensor()
    {
        // Arrange
        var sensor = new Measurements.Api.Domain.Entities.Sensor { /* initialize sensor properties here */ };
        sensor = await Persist(sensor);

        // Act
        var response = await Client.DeleteAsync($"/api/sensors/{sensor.Id}");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        var deletedSensor = await Context.Sensors.FindAsync(sensor.Id);
        deletedSensor.Should().BeNull();
    }
}
