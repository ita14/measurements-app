namespace Measurements.IntegrationTests;

/// <summary>
/// Defines xUnit.dotnet fixture collection that is shared between test classes.
/// Each test project needs to define its own collection.
/// </summary>
[CollectionDefinition("integrations-tests")]
public class TestContainerFixtureCollection : ICollectionFixture<IntegrationTestWebApplicationFactory>
{
}
