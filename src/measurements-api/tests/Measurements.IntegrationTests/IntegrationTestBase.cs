using Measurements.Api.Domain.Entities;
using Measurements.Api.Infrastructure.Context;
using Microsoft.Extensions.DependencyInjection;

namespace Measurements.IntegrationTests;

[Collection("integrations-tests")]
public class IntegrationTestBase
{
    private readonly IntegrationTestWebApplicationFactory _factory;

    protected HttpClient Client { get; }

    protected MeasurementsDbContext Context => GetScopedService<MeasurementsDbContext>();

    protected IntegrationTestBase(IntegrationTestWebApplicationFactory factory)
    {
        _factory = factory;
        Client = _factory.CreateClient();
        ResetData();
    }

    protected T GetScopedService<T>() where T : notnull
    {
        return _factory.Services.CreateScope().ServiceProvider.GetRequiredService<T>();
    }

    protected async Task<T> Persist<T>(T item) where T : BaseEntity
    {
        var context = Context;
        context.Set<T>().Add(item);
        await context.SaveChangesAsync();
        return item;
    }

    protected async Task<IEnumerable<T>> Persist<T>(IEnumerable<T> items) where T : BaseEntity
    {
        var context = Context;
        var entities = items.ToList();
        context.Set<T>().AddRange(entities);
        await context.SaveChangesAsync();
        return entities;
    }

    private void ResetData()
    {
        using var scope = _factory.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<MeasurementsDbContext>();

        db.Database.EnsureDeleted();
        db.Database.EnsureCreated();
    }
}
